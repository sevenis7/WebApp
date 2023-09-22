﻿using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public enum RequestStatus
    {
        Receieved = 0,
        InWork,
        Completed,
        Rejected,
        Canceled
    }

    public class Request
    {
        public int RequestId { get; set; }

        public string Text { get; set; } = null!;

        public RequestStatus Status { get; set; }

        public DateTime DateTime { get; set; }

        //references

        public User User { get; set; } = null!;

        public Guid UserId { get; set; }
    }
}