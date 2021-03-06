﻿using MailSender.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Models
{
    public class Server: NamedEntity
    {
        public string Address { get; set; }
        private int _port = 25;
        public int Port
        {
            get => _port;
            set
            {
                if (value < 0 || value >= 65535)
                    throw new ArgumentOutOfRangeException
                        (nameof(value), value, "Номер порта должен быть в диапазоне от 0 до 65534");
                _port = value;
            }
        }
        public bool UseSSL { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}
