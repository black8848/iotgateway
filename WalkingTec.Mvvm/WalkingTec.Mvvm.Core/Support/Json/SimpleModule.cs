using System;
using System.Collections.Generic;

namespace WalkingTec.Mvvm.Core.Support.Json
{
    public class SimpleModule
    {
        public Guid ID { get; set; }

        public ActionDescriptionAttribute ActionDes { get; set; }

        public string _name;

        public string ModuleName
        {
            get
            {
                if (_name == null)
                {
                    if (ActionDes?._localizer != null && string.IsNullOrEmpty(ActionDes?.Description) == false)
                    {
                        _name = ActionDes._localizer[ActionDes.Description];
                    }
                    else
                    {
                        _name = _name ?? "";
                    }
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string ClassName { get; set; }

        public List<SimpleAction> Actions { get; set; }

        public Guid? AreaId { get; set; }
        public SimpleArea Area { get; set; }

        public string NameSpace { get; set; }

        public bool IgnorePrivillege { get; set; }

        public bool MainHostOnly { get; set; }
        public bool IsApi { get; set; }

        public string FullName
        {
            get
            {
                return this.NameSpace + "," + this.ClassName;
            }
        }
    }
}