﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    internal class GuestGrade : InitialProject.Serializer.ISerializable
    {

        public string GuestUserName { get; set; }
        public int Cleanliness { get; set; }
        public int RespectingRules { get; set; }
        public string CommentText { get; set; }

        public GuestGrade() { }

        public GuestGrade(string guestUserName, int cleanilness, int respectingRules, string commentText)
        {
            GuestUserName = guestUserName;
            Cleanliness = cleanilness;
            RespectingRules = respectingRules;
            CommentText = commentText;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { GuestUserName, Cleanliness.ToString(), RespectingRules.ToString(), CommentText };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestUserName = values[0];
            Cleanliness = Convert.ToInt32(values[1]);
            RespectingRules = Convert.ToInt32(values[2]);
            CommentText = values[3];
        }

    }
}
