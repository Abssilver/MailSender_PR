using MailSender.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MailSender.Models
{
    public class Recipient: Person, IDataErrorInfo
    {
        string IDataErrorInfo.this[string propertyName]
        {
            get 
            {
                switch (propertyName)
                {
                    default: return null;
                    case nameof(Name):
                        var name = Name;
                        if (name.Equals(string.Empty)) return "Поле имя не заполнено!";
                        if (name.Length < 2) return "Введено слишком короткое имя!";
                        if (name.Length > 20) return "Введено слишком длинное имя!";

                        return null;
                    case nameof(Address):
                        return null;
                }
            }
        }
        string IDataErrorInfo.Error { get; } = null;

        public string Description { get; set; }
        public override string Name
        {
            get => base.Name;
            set
            {
                /*
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                
                if (value == "")
                {
                    throw new ArgumentException("Поле не может быть пустым!", nameof(value));
                }
                */
                base.Name = value;
            }
        }
    }
}
