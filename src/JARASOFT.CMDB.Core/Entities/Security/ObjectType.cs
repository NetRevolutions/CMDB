using System;
using System.Runtime.Serialization;

namespace JARASOFT.CMDB.Core.Entities.Security
{
    [Serializable]
    [DataContract(Namespace = "http://schemas.datacontract.org/2018/03/JARASOFT.Entites")]
    public enum ObjectType
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Menu = 1,
        [EnumMember]
        Page = 2,
        [EnumMember]
        PageAction = 3,
        [EnumMember]
        Widget = 4,
        [EnumMember]
        Queues = 5,
        [EnumMember]
        Reports = 6,
        [EnumMember]
        Dashboard = 7,
        [EnumMember]
        Search = 8,
        [EnumMember]
        KnowledgeBase = 10,
        [EnumMember]
        QuickSearch = 15,
        [EnumMember]
        Customer = 20,
        [EnumMember]
        Contact = 21,
        [EnumMember]
        CSSCompany = 27,
        [EnumMember]
        View = 32,
        [EnumMember]
        CSSCustomer = 33,
        [EnumMember]
        CSSApplications = 34,
        [EnumMember]
        CSSReport = 35,
        [EnumMember]
        CSSAdministration = 36,
        [EnumMember]
        CSSContactPrivileges = 37,
        [EnumMember]
        ProjectType = 40,
        [EnumMember]
        Integration = 50,
        [EnumMember]
        DateFormat = 96,
        [EnumMember]
        MyAccount = 97,
        [EnumMember]
        Admin = 98,
        [EnumMember]
        TimeZone = 99,
        [EnumMember]
        Notification = 100,
        [EnumMember]
        ResourceCalendar = 101,
        [EnumMember]
        UserDateFormat = 102,
        [EnumMember]
        UserApplication = 103,
        [EnumMember]
        DefaultCalendarGroups = 104,
        [EnumMember]
        DefaultCustomer = 200,
        [EnumMember]
        Administration = 201,
        [EnumMember]
        DigitGroupingSymbol = 203,
        [EnumMember]
        DecimalSymbol = 204,
        [EnumMember]
        DefaultCreatorGroup = 205,
    }
}
