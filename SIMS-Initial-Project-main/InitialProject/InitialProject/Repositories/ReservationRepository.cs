﻿using InitialProject.Domain.Model;
using InitialProject.Domain.Models;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repositories;
using InitialProject.Serializer;
using InitialProject.WPF.Views.Owner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using LiveCharts.Wpf;
using LiveCharts;

namespace InitialProject.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<Reservation> _serializer;

        private AccommodationRepository _accommodationRepository;

        private readonly NotificationRepository _notificationRepository;

        private RenovationRepository renovationRepository;

        private List<Reservation> _reservations;

        private UserRepository _userRepository;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _accommodationRepository = new AccommodationRepository();
            _reservations = _serializer.FromCSV(FilePath);
            _userRepository = new UserRepository();
            _notificationRepository = new NotificationRepository();
            renovationRepository = new RenovationRepository();
        }

        public Reservation Update(Reservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            Reservation current = _reservations.Find(c => c.Id == reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public void LogicalDeleteExpire(Reservation reservation)
        {
            if (reservation.GradeStatus != "Graded" && reservation.GradeStatus != "Expired")
            {
                reservation.GradeStatus = "Expire";
                Update(reservation);
            }
        }

        public void LogicalDelete(Reservation reservation)
        {
            reservation.GradeStatus = "Graded";
            Update(reservation);
        }


        public int GetLastId()
        {
            return _reservations.Max(reservation => reservation.Id);
        }

        public List<Reservation> GetAll()
        {
            return _reservations;
        }

        public Reservation GetReservationById(int id)
        {
            _reservations = _serializer.FromCSV(FilePath);
            return _reservations.Find(r => r.Id == id);
        }


        public ColumnSeries ShowHotelDataInChart(int accommodationId)
        {
            DateTime dateTime = DateTime.Now;
            List<int> yValues = new List<int>();
            List<Reservation> reservations = GetAll();
            for (int i = 0; i < 5; i++)
            {
                int tempCount = 0;
                foreach (Reservation reservation in reservations)
                {
                    if (reservation.AccommodationId == accommodationId && reservation.DateFrom.Year == dateTime.Year - 4 + i)
                    {
                        tempCount++;
                    }
                }
                yValues.Add(tempCount);
            }
            ColumnSeries columnSeries = new ColumnSeries();
            columnSeries.Title = accommodationId.ToString();
            columnSeries.Values = new ChartValues<int>(yValues);

            return columnSeries;
        }

        public List<int> ShowHotelDataPerMonth(int accommodationId, int year)
        {
            List<int> yValues = new List<int>();
            List<Reservation> reservations = GetAll();
            for (int i = 1; i <= 12; i++)
            {
                int tempCount = 0;
                foreach (Reservation reservation in reservations)
                {
                    if (reservation.AccommodationId == accommodationId&& reservation.DateFrom.Month == i && year == reservation.DateFrom.Year)
                    {
                        tempCount++;
                    }
                }
                yValues.Add(tempCount);
            }
            return yValues;
        }



        public void ReserveRenovation(ComboBox comboBox, DateTime startDate, DateTime endDate)
        {
            int selectedAccommodationId = 0; // Default value or handle the case when no value is selected

            if (comboBox.SelectedValue is int accommodationId)
            {
                selectedAccommodationId = accommodationId;
            }
            else if (comboBox.Tag is int tagAccommodationId)
            {
                selectedAccommodationId = tagAccommodationId;
            }

            List<Reservation> reservations = GetAll();

            if (!(startDate > endDate))
            {
                if (IsAccommodationAvailable(reservations, selectedAccommodationId, startDate, endDate))
                {
                    Renovation renovation = new Renovation(
                        renovationRepository.NextId(),
                        _accommodationRepository.GetAccommodationById(selectedAccommodationId),
                        startDate,
                        endDate);

                    renovationRepository.Save(renovation);
                    MessageBox.Show("Success");
                }
            }   
            else
            {
                MessageBox.Show("Error");
            }
        }

        public bool IsAccommodationAvailable(List<Reservation> reservations, int accommodationId, DateTime startDate, DateTime endDate)
        {
            Accommodation accommodation = _accommodationRepository.GetAccommodationById(accommodationId);

            foreach (Reservation reservation in reservations)
            {
                if (reservation.AccommodationId == accommodationId && reservation.DateFrom < endDate && startDate < reservation.DateTo)
                {
                    return false;
                }
            }
            return true;
        }




        public List<ReservationAccommodation> GetAllByGuestId(int GuestId)
        {
            List<Reservation> guestReservations = _reservations.FindAll(reservation => reservation.GuestId == GuestId);

            List<ReservationAccommodation> reservationAccommodations = new List<ReservationAccommodation>();

            foreach (Reservation reservation in guestReservations)
            {
                Accommodation accommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);
                ReservationAccommodation reservationAccommodation = new ReservationAccommodation(accommodation, reservation);
                reservationAccommodations.Add(reservationAccommodation);
            }

            return reservationAccommodations;
        }

        private bool IsAvailable(List<Reservation> accommodationReservations, DateTime startDate, DateTime endDate)
        {
            foreach (Reservation reservation in accommodationReservations)
            {
                if (reservation.DateFrom < endDate && startDate < reservation.DateTo)
                {
                    return false;
                }
            }
            return true;
        }

        public List<ReservationDate> GetReservationsForGuest(int guestId, int accommodationId, DateTime fromDate, DateTime toDate, int daysNumber)
        {
            List<Reservation> accommodationReservations = _reservations.FindAll(reservation => reservation.AccommodationId == accommodationId);
            List<ReservationDate> availableDates = new List<ReservationDate>();
            DateTime currentDate = fromDate;

            while (currentDate.AddDays(daysNumber) <= toDate)
            {
                DateTime endDate = currentDate.AddDays(daysNumber);

                if (IsAvailable(accommodationReservations, currentDate, endDate))
                {
                    availableDates.Add(new ReservationDate(currentDate, endDate));
                }

                currentDate = currentDate.AddDays(1);
            }

            if (availableDates.Count == 0)
            {
                DateTime firstAvailableDate = fromDate;

                while (!IsAvailable(accommodationReservations, firstAvailableDate, firstAvailableDate.AddDays(daysNumber)))
                {
                    firstAvailableDate = firstAvailableDate.AddDays(1);
                }

                availableDates.Add(new ReservationDate(firstAvailableDate, firstAvailableDate.AddDays(daysNumber)));
            }

            return availableDates;
        }


        public List<DateTime> FindAlternativeDates(int accommodationId, DateTime checkInDate, DateTime checkOutDate, int numberOfDays)
        {
            List<DateTime> alternativeDates = new List<DateTime>();
            List<Reservation> reservations = GetAll();
            DateTime startDate = checkInDate.AddDays(1);
            DateTime endDate = checkOutDate.AddDays(30);
            while (startDate < endDate)
            {
                if (IsAccommodationAvailable(reservations, accommodationId, startDate, startDate.AddDays(numberOfDays)))
                {
                    alternativeDates.Add(startDate);
                    if (alternativeDates.Count == 5)
                    {
                        break;
                    }
                }
                startDate = startDate.AddDays(1);
            }

            return alternativeDates;
        }


        public List<Accommodation> IsAccommodationRenovated()
        {
            DateTime dateTime = DateTime.Now;
            List<Accommodation> accommodations = _accommodationRepository.GetAllAccomodations();
            List<Accommodation> changedHotels = new List<Accommodation>();
            List<Renovation> renovations = renovationRepository.GetAll();
            foreach (Accommodation accommodation in accommodations)
            {
                foreach (Renovation renovation in renovations)
                {
                    if (accommodation.Id == renovation.Accommodation.Id)
                    {
                        if (renovation.StartDate <= dateTime && dateTime <= renovation.EndDate)
                        {
                            accommodation.RenovationStatus = "IsRenovating";
                            changedHotels.Add(accommodation);
                        }
                        else if (dateTime >= renovation.EndDate && dateTime.Year - renovation.EndDate.Year < 1)
                        {
                            accommodation.RenovationStatus = "Renovated";
                            changedHotels.Add(accommodation);
                        }
                        else if (dateTime < renovation.EndDate && dateTime.Year - renovation.EndDate.Year == 0)
                        {
                            accommodation.RenovationStatus = "NotRenovated";
                            changedHotels.Add(accommodation);
                        }
                        else
                        {
                            accommodation.RenovationStatus = "NotRenovated";
                            changedHotels.Add(accommodation);
                            renovationRepository.Delete(renovation);
                        }

                    }
                    if (renovations.Count == 0) break;
                }
            }
            return changedHotels;
        }
        public void ChangeAllRenovatedStatus()
        {
            List<Accommodation> accommodations = IsAccommodationRenovated();
            foreach (Accommodation accommodation in accommodations)
            {
                _accommodationRepository.Update(accommodation);
            }
        }


        public string CheckGuests(Reservation reservation, int guestsNumber)
        {
            Accommodation accommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);

            if (guestsNumber > accommodation.GuestsNumber)
            {
                return "Broj gostiju prekoracuje dozvoljeni broj gostiju";
            }
            else
            {
                reservation.GuestsNumber = guestsNumber;
                return this.SaveReservation(reservation);
            }
        }

        public string DeleteReservation(Reservation reservation)
        {
            DateTime currentDate = DateTime.Now;
            Accommodation reservationAccommodation = _accommodationRepository.GetAccommodationById(reservation.AccommodationId);

            if (reservationAccommodation.ReservationDays > 0 &&
                reservation.DateFrom.AddDays(-reservationAccommodation.ReservationDays) < currentDate)
            {
                return "Rezervaciju nije moguce otkazati";
            }
            else if ((reservation.DateFrom - currentDate).TotalHours < 24)
            {
                return "Rezervaciju nije moguce otkazati";
            }

            Notification notification = new Notification();
            notification.Message = "Guest has canceled reservation: " + reservation.Id;
            notification.HasRead = false;
            notification.UserId = _accommodationRepository.GetAccommodationById(reservation.AccommodationId).OwnerId;
            _notificationRepository.Save(notification);

            _reservations.Remove(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return "Uspesno obrisana rezervacija!";
        }

        public void CheckIfSuperGuest(int GuestId)
        {

            User user = _userRepository.GetById(GuestId);
            if (user != null && user.IsSuperGuest && user.Points > 0)
            {
                this.DecrementPoints(user);
            }
            else if (user != null && !user.IsSuperGuest)
            {
                this.CheckIfGainedSuperGuest(user);
            }
        }

        public void DecrementPoints(User user)
        {
            user.Points--;
            _userRepository.UpdateUser(user);
        }

        public void CheckIfGainedSuperGuest(User user)
        {
            List<Reservation> userReservations = _reservations.FindAll(r => r.GuestId == user.Id && r.DateFrom > DateTime.Now.AddYears(-1) && r.DateTo < DateTime.Now);
            if (userReservations.Count >= 1)
            {
                this.SetSuperGuest(user);
            }
        }

        public void SetSuperGuest(User user)
        {
            user.DateSuperGuest = DateTime.Now;
            user.IsSuperGuest = true;
            user.Points = 5;
            _userRepository.UpdateUser(user);
        }

        public String SaveReservation(Reservation reservation)
        {

            bool renovationInProcess = this.CheckRenovationInProcess(reservation.AccommodationId);

            if (renovationInProcess) { return "Smestaj je u procesu renoviranja"; }

            this.CheckIfSuperGuest(reservation.GuestId);

            int reservationId = this.GetLastId();
            reservation.Id = reservationId + 1;

            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);

            return "Rezervacija je uspesno sacuvana!";
        }
        private bool CheckRenovationInProcess(int accommodationId)
        {
            Accommodation accommodation = _accommodationRepository.GetAccommodationById(accommodationId);
            if (accommodation != null && accommodation.RenovationStatus == "IsRenovating")
            {
                return true;
            }
            return false;
        }

        public List<Reservation> ReadFromReservationsCsv(string FileName)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    string[] fields = line.Split('|');
                    Reservation reservation = new Reservation();
                    reservation.Id = Convert.ToInt32(fields[0]);
                    reservation.GuestId = Convert.ToInt32(fields[1]);
                    reservation.GuestUserName = fields[2];
                    reservation.AccommodationId = Convert.ToInt32(fields[3]);
                    reservation.DateFrom = Convert.ToDateTime(fields[4]);
                    reservation.DateTo = Convert.ToDateTime(fields[5]);
                    reservation.DaysNumber = Convert.ToInt32(fields[6]);
                    reservation.GuestsNumber = Convert.ToInt32(fields[7]);
                    reservations.Add(reservation);

                }
            }
            return reservations;
        }
    }
}
