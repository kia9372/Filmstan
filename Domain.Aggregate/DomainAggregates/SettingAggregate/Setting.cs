using Domain.Core.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Aggregate.DomainAggregates.SettingAggregate
{
    public class Setting : Aggregates , IAggregateMarker
    {
        private string _key;
        private string _settingValue;

        [Required]
        public string Key { get => _key; private set => SetWithNotify(value, ref _key); }
        [Required]
        public string SettingValue { get => _settingValue; private set => SetWithNotify(value, ref _settingValue); }

        public Setting(string key, string settingValue)
        {
            SetProperties(key, settingValue);
        }

        public void SetProperties(string key, string settingValue)
        {
            Key = key;
            SettingValue = settingValue;
        }

        public void SetSettingValue(string settingValue)
        {
            SettingValue = settingValue;
        }
    }
}
