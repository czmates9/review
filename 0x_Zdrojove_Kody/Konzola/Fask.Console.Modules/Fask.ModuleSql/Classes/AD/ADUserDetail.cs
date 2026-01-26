using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Fask.ModuleSql.Classes.AD
{
    public class ADUserDetail
    {
        private String _firstName;
        private String _middleName;
        private String _lastName;
        private String _loginName;
        private String _loginNameWithDomain;
        private String _streetAddress;
        private String _city;
        private String _state;
        private String _postalCode;
        private String _country;
        private String _homePhone;
        private String _extension;
        private String _mobile;
        private String _fax;
        private String _emailAddress;
        private String _title;
        private String _company;
        private String _manager;
        private String _managerName;
        private String _department;

        #region TaD pridane

        private String _INFO;
        private String _OBJECTCLASS;
        private String _CONTAINERNAME;
        private String _COUNTRYNOTATION;
        private String _PHYSICALDELIVERYOFFICENAME;
        private String _DISTINGUISHEDNAME;
        private String _INSTANCETYPE;
        private String _WHENCREATED;
        private String _WHENCHANGED;
        private String _DISPLAYNAME;
        private String _ID;
        private String _USNCREATED;
        private String _MEMBEROF;
        private String _USNCHANGED;
        private String _PROXYADDRESSES;
        private String _DIRECTREPORTS;
        private String _NAME;
        private Guid _OBJECTGUID;
        private String _USERACCOUNTCONTROL;
        private String _BADPWDCOUNT;
        private String _CODEPAGE;
        private String _COUNTRYCODE;
        private String _BADPASSWORDTIME;
        private String _LASTLOGOFF;
        private String _LASTLOGON;
        private String _PWDLASTSET;
        private String _PRIMARYGROUPID;
        private String _OBJECTSID;
        private String _ADMINCOUNT;
        private String _ACCOUNTEXPIRES;
        private String _LOGONCOUNT;
        private String _SAMACCOUNTTYPE;
        private String _SHOWINADDRESSBOOK;
        private String _LEGACYEXCHANGEDN;
        private String _SERVICEPRINCIPALNAME;
        private String _OBJECTCATEGORY;
        private String _DSCOREPROPAGATIONDATA;
        private String _LASTLOGONTIMESTAMP;
        private String _PAGER;
        private String _MSEXCHUSERACCOUNTCONTROL;
        private String _MDBUSEDEFAULTS;
        private String _MSEXCHMAILBOXSECURITYDESCRIPTOR;
        private String _HOMEMDB;
        private String _MSEXCHPOLICIESINCLUDED;
        private String _HOMEMTA;
        private String _MSEXCHRECIPIENTTYPEDETAILS;
        private String _MAILNICKNAME;
        private String _MSEXCHHOMESERVERNAME;
        private String _MSEXCHVERSION;
        private String _MSEXCHRECIPIENTDISPLAYTYPE;
        private String _MSEXCHMAILBOXGUID;
        private String _NTSECURITYDESCRIPTOR;

        #endregion


        public String Department
        {
            get { return _department; }
        }

        public String FirstName
        {
            get { return _firstName; }
        }

        public String MiddleName
        {
            get { return _middleName; }
        }

        public String LastName
        {
            get { return _lastName; }
        }

        public String LoginName
        {
            get { return _loginName; }
        }

        public String LoginNameWithDomain
        {
            get { return _loginNameWithDomain; }
        }

        public String StreetAddress
        {
            get { return _streetAddress; }
        }

        public String City
        {
            get { return _city; }
        }

        public String State
        {
            get { return _state; }
        }

        public String PostalCode
        {
            get { return _postalCode; }
        }

        public String Country
        {
            get { return _country; }
        }

        public String HomePhone
        {
            get { return _homePhone; }
        }

        public String Extension
        {
            get { return _extension; }
        }

        public String Mobile
        {
            get { return _mobile; }
        }

        public String Fax
        {
            get { return _fax; }
        }

        public String EmailAddress
        {
            get { return _emailAddress; }
        }

        public String Title
        {
            get { return _title; }
        }

        public String Company
        {
            get { return _company; }
        }

        #region TaD pridane

        public String INFO
        {
            get { return _INFO; }
        }

        public String OBJECTCLASS
        {
            get { return _OBJECTCLASS; }
        }
        public String CONTAINERNAME
        {
            get { return _CONTAINERNAME; }
        }
        public String COUNTRYNOTATION
        {
            get { return _COUNTRYNOTATION; }
        }
        public String PHYSICALDELIVERYOFFICENAME
        {
            get { return _PHYSICALDELIVERYOFFICENAME; }
        }
        public String DISTINGUISHEDNAME
        {
            get { return _DISTINGUISHEDNAME; }
        }

        public String INSTANCETYPE
        {
            get { return _INSTANCETYPE; }
        }
        public String WHENCREATED
        {
            get { return _WHENCREATED; }
        }
        public String WHENCHANGED
        {
            get { return _WHENCHANGED; }
        }
        public String DISPLAYNAME
        {
            get { return _DISPLAYNAME; }
        }
        public String ID
        {
            get { return _ID; }
        }
        public String USNCREATED
        {
            get { return _USNCREATED; }
        }
        public String MEMBEROF
        {
            get { return _MEMBEROF; }
        }
        public String USNCHANGED
        {
            get { return _USNCHANGED; }
        }
        public String PROXYADDRESSES
        {
            get { return _PROXYADDRESSES; }
        }
        public String DIRECTREPORTS
        {
            get { return _DIRECTREPORTS; }
        }

        public String NAME
        {
            get { return _NAME; }
        }

        public Guid OBJECTGUID
        {
            get { return _OBJECTGUID; }
        }

        public String USERACCOUNTCONTROL
        {
            get { return _USERACCOUNTCONTROL; }
        }

        public String BADPWDCOUNT
        {
            get { return _BADPWDCOUNT; }
        }

        public String CODEPAGE
        {
            get { return _CODEPAGE; }
        }

        public String COUNTRYCODE
        {
            get { return _COUNTRYCODE; }
        }

        public String BADPASSWORDTIME
        {
            get { return _BADPASSWORDTIME; }
        }

        public String LASTLOGOFF
        {
            get { return _LASTLOGOFF; }
        }

        public String LASTLOGON
        {
            get { return _LASTLOGON; }
        }

        public String PWDLASTSET
        {
            get { return _PWDLASTSET; }
        }

        public String PRIMARYGROUPID
        {
            get { return _PRIMARYGROUPID; }
        }

        public String OBJECTSID
        {
            get { return _OBJECTSID; }
        }

        public String ADMINCOUNT
        {
            get { return _ADMINCOUNT; }
        }

        public String ACCOUNTEXPIRES
        {
            get { return _ACCOUNTEXPIRES; }
        }

        public String LOGONCOUNT
        {
            get { return _LOGONCOUNT; }
        }

        public String SAMACCOUNTTYPE
        {
            get { return _SAMACCOUNTTYPE; }
        }

        public String SHOWINADDRESSBOOK
        {
            get { return _SHOWINADDRESSBOOK; }
        }

        public String LEGACYEXCHANGEDN
        {
            get { return _LEGACYEXCHANGEDN; }
        }

        public String SERVICEPRINCIPALNAME
        {
            get { return _SERVICEPRINCIPALNAME; }
        }

        public String OBJECTCATEGORY
        {
            get { return _OBJECTCATEGORY; }
        }

        public String DSCOREPROPAGATIONDATA
        {
            get { return _DSCOREPROPAGATIONDATA; }
        }

        public String LASTLOGONTIMESTAMP
        {
            get { return _LASTLOGONTIMESTAMP; }
        }

        public String PAGER
        {
            get { return _PAGER; }
        }

        public String MSEXCHUSERACCOUNTCONTROL
        {
            get { return _MSEXCHUSERACCOUNTCONTROL; }
        }

        public String MDBUSEDEFAULTS
        {
            get { return _MDBUSEDEFAULTS; }
        }

        public String MSEXCHMAILBOXSECURITYDESCRIPTOR
        {
            get { return _MSEXCHMAILBOXSECURITYDESCRIPTOR; }
        }

        public String HOMEMDB
        {
            get { return _HOMEMDB; }
        }

        public String MSEXCHPOLICIESINCLUDED
        {
            get { return _MSEXCHPOLICIESINCLUDED; }
        }

        public String HOMEMTA
        {
            get { return _HOMEMTA; }
        }

        public String MSEXCHRECIPIENTTYPEDETAILS
        {
            get { return _MSEXCHRECIPIENTTYPEDETAILS; }
        }

        public String MAILNICKNAME
        {
            get { return _MAILNICKNAME; }
        }

        public String MSEXCHHOMESERVERNAME
        {
            get { return _MSEXCHHOMESERVERNAME; }
        }

        public String MSEXCHVERSION
        {
            get { return _MSEXCHVERSION; }
        }

        public String MSEXCHRECIPIENTDISPLAYTYPE
        {
            get { return _MSEXCHRECIPIENTDISPLAYTYPE; }
        }

        public String MSEXCHMAILBOXGUID
        {
            get { return _MSEXCHMAILBOXGUID; }
        }

        public String NTSECURITYDESCRIPTOR
        {
            get { return _NTSECURITYDESCRIPTOR; }
        }

        #endregion



        public ADUserDetail Manager
        {
            get
            {
                if (!String.IsNullOrEmpty(_managerName))
                {
                    ActiveDirectoryHelper ad = new ActiveDirectoryHelper();
                    return ad.GetUserByFullName(_managerName);
                }
                return null;
            }
        }

        public String ManagerName
        {
            get { return _managerName; }
        }


        private ADUserDetail(DirectoryEntry directoryUser)
        {

            String domainAddress;
            String domainName;
            _firstName = GetProperty(directoryUser, ADProperties.FIRSTNAME);
            _middleName = GetProperty(directoryUser, ADProperties.MIDDLENAME);
            _lastName = GetProperty(directoryUser, ADProperties.LASTNAME);
            _loginName = GetProperty(directoryUser, ADProperties.LOGINNAME);
            String userPrincipalName = GetProperty(directoryUser, ADProperties.USERPRINCIPALNAME);
            if (!string.IsNullOrEmpty(userPrincipalName))
            {
                domainAddress = userPrincipalName.Split('@')[1];
            }
            else
            {
                domainAddress = String.Empty;
            }

            if (!string.IsNullOrEmpty(domainAddress))
            {
                domainName = domainAddress.Split('.').First();
            }
            else
            {
                domainName = String.Empty;
            }
            _loginNameWithDomain = String.Format(@"{0}\{1}", domainName, _loginName);
            _streetAddress = GetProperty(directoryUser, ADProperties.STREETADDRESS);
            _city = GetProperty(directoryUser, ADProperties.CITY);
            _state = GetProperty(directoryUser, ADProperties.STATE);
            _postalCode = GetProperty(directoryUser, ADProperties.POSTALCODE);
            _country = GetProperty(directoryUser, ADProperties.COUNTRY);
            _company = GetProperty(directoryUser, ADProperties.COMPANY);
            _department = GetProperty(directoryUser, ADProperties.DEPARTMENT);
            _homePhone = GetProperty(directoryUser, ADProperties.HOMEPHONE);
            _extension = GetProperty(directoryUser, ADProperties.EXTENSION);
            _mobile = GetProperty(directoryUser, ADProperties.MOBILE);
            _fax = GetProperty(directoryUser, ADProperties.FAX);
            _emailAddress = GetProperty(directoryUser, ADProperties.EMAILADDRESS);
            _title = GetProperty(directoryUser, ADProperties.TITLE);
            _manager = GetProperty(directoryUser, ADProperties.MANAGER);
            if (!String.IsNullOrEmpty(_manager))
            {
                String[] managerArray = _manager.Split(',');
                _managerName = managerArray[0].Replace("CN=", "");
            }

            #region Data

            _INFO = GetProperty(directoryUser, ADProperties.INFO);
            _OBJECTCLASS = GetProperty(directoryUser, ADProperties.OBJECTCLASS);
            _CONTAINERNAME = GetProperty(directoryUser, ADProperties.CONTAINERNAME);
            _COUNTRYNOTATION = GetProperty(directoryUser, ADProperties.COUNTRYNOTATION);
            _PHYSICALDELIVERYOFFICENAME = GetProperty(directoryUser, ADProperties.PHYSICALDELIVERYOFFICENAME);
            _DISTINGUISHEDNAME = GetProperty(directoryUser, ADProperties.DISTINGUISHEDNAME);
            _INSTANCETYPE = GetProperty(directoryUser, ADProperties.INSTANCETYPE);
            _WHENCREATED = GetProperty(directoryUser, ADProperties.WHENCREATED);
            _WHENCHANGED = GetProperty(directoryUser, ADProperties.WHENCHANGED);
            _DISPLAYNAME = GetProperty(directoryUser, ADProperties.DISPLAYNAME);
            _ID = GetProperty(directoryUser, ADProperties.ID);



            _USNCREATED = GetProperty(directoryUser, ADProperties.USNCREATED);
            _MEMBEROF = GetProperty(directoryUser, ADProperties.MEMBEROF);
            _USNCHANGED = GetProperty(directoryUser, ADProperties.USNCHANGED);
            _PROXYADDRESSES = GetProperty(directoryUser, ADProperties.PROXYADDRESSES);
            _DIRECTREPORTS = GetProperty(directoryUser, ADProperties.DIRECTREPORTS);
            _NAME = GetProperty(directoryUser, ADProperties.NAME);
            _OBJECTGUID = GetPropertyGuid(directoryUser, ADProperties.OBJECTGUID);
            _USERACCOUNTCONTROL = GetProperty(directoryUser, ADProperties.USERACCOUNTCONTROL);
            _BADPWDCOUNT = GetProperty(directoryUser, ADProperties.BADPWDCOUNT);
            _CODEPAGE = GetProperty(directoryUser, ADProperties.CODEPAGE);
            _COUNTRYCODE = GetProperty(directoryUser, ADProperties.COUNTRYCODE);
            _BADPASSWORDTIME = GetProperty(directoryUser, ADProperties.BADPASSWORDTIME);
            _LASTLOGOFF = GetProperty(directoryUser, ADProperties.LASTLOGOFF);
            _LASTLOGON = GetProperty(directoryUser, ADProperties.LASTLOGON);
            _PWDLASTSET = GetProperty(directoryUser, ADProperties.PWDLASTSET);
            _PRIMARYGROUPID = GetProperty(directoryUser, ADProperties.PRIMARYGROUPID);
            _OBJECTSID = GetProperty(directoryUser, ADProperties.OBJECTSID);
            _ADMINCOUNT = GetProperty(directoryUser, ADProperties.ADMINCOUNT);
            _ACCOUNTEXPIRES = GetProperty(directoryUser, ADProperties.ACCOUNTEXPIRES);
            _LOGONCOUNT = GetProperty(directoryUser, ADProperties.LOGONCOUNT);
            _SAMACCOUNTTYPE = GetProperty(directoryUser, ADProperties.SAMACCOUNTTYPE);
            _SHOWINADDRESSBOOK = GetProperty(directoryUser, ADProperties.SHOWINADDRESSBOOK);
            _LEGACYEXCHANGEDN = GetProperty(directoryUser, ADProperties.LEGACYEXCHANGEDN);
            _SERVICEPRINCIPALNAME = GetProperty(directoryUser, ADProperties.SERVICEPRINCIPALNAME);
            _OBJECTCATEGORY = GetProperty(directoryUser, ADProperties.OBJECTCATEGORY);
            _DSCOREPROPAGATIONDATA = GetProperty(directoryUser, ADProperties.DSCOREPROPAGATIONDATA);
            _LASTLOGONTIMESTAMP = GetProperty(directoryUser, ADProperties.LASTLOGONTIMESTAMP);
            _PAGER = GetProperty(directoryUser, ADProperties.PAGER);
            _MSEXCHUSERACCOUNTCONTROL = GetProperty(directoryUser, ADProperties.MSEXCHUSERACCOUNTCONTROL);
            _MDBUSEDEFAULTS = GetProperty(directoryUser, ADProperties.MDBUSEDEFAULTS);
            _MSEXCHMAILBOXSECURITYDESCRIPTOR = GetProperty(directoryUser, ADProperties.MSEXCHMAILBOXSECURITYDESCRIPTOR);
            _HOMEMDB = GetProperty(directoryUser, ADProperties.HOMEMDB);
            _MSEXCHPOLICIESINCLUDED = GetProperty(directoryUser, ADProperties.MSEXCHPOLICIESINCLUDED);
            _HOMEMTA = GetProperty(directoryUser, ADProperties.HOMEMTA);
            _MSEXCHRECIPIENTTYPEDETAILS = GetProperty(directoryUser, ADProperties.MSEXCHRECIPIENTTYPEDETAILS);
            _MAILNICKNAME = GetProperty(directoryUser, ADProperties.MAILNICKNAME);
            _MSEXCHHOMESERVERNAME = GetProperty(directoryUser, ADProperties.MSEXCHHOMESERVERNAME);
            _MSEXCHVERSION = GetProperty(directoryUser, ADProperties.MSEXCHVERSION);
            _MSEXCHRECIPIENTDISPLAYTYPE = GetProperty(directoryUser, ADProperties.MSEXCHRECIPIENTDISPLAYTYPE);
            _MSEXCHMAILBOXGUID = GetProperty(directoryUser, ADProperties.MSEXCHMAILBOXGUID);
            _NTSECURITYDESCRIPTOR = GetProperty(directoryUser, ADProperties.NTSECURITYDESCRIPTOR);
            #endregion
        }


        private static String GetProperty(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {

                return userDetail.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private static Guid GetPropertyGuid(DirectoryEntry userDetail, String propertyName)
        {
            if (userDetail.Properties.Contains(propertyName))
            {
                object tmp = userDetail.Properties[propertyName][0];
                byte[] arr = (byte[])tmp;
                return new Guid(arr);
            }
            else
            {
                var sevenItems = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                return new Guid(sevenItems);
            }
        }


        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static ADUserDetail GetUser(DirectoryEntry directoryUser)
        {
            return new ADUserDetail(directoryUser);
        }
    }
}
