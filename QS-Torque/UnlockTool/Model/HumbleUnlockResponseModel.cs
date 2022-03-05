using System;
using FrameworksAndDrivers.Gui.Wpf;
using UnlockToolShared.Entities;

namespace UnlockTool.Model
{
    public class HumbleSpecificLicenseModel : BindableBase
    {
        private SpecificLicense license_;

        public SpecificLicense getEntity()
        {
            return license_;
        }

        public HumbleSpecificLicenseModel(SpecificLicense license)
        {
            license_ = license;
        }

        public string LicenseName
        {
            get { return license_.LicenseName; }
            set { license_.LicenseName = value; RaisePropertyChanged(); }
        }

        public string PcFqdns
        {
            get
            {
                if (license_.PcFqdns.Count == 0)
                {
                    return "";
                }
                return license_.PcFqdns[0];


            }
            set
            {
                if (license_.PcFqdns.Count == 0 && value.Length > 0)
                {
                    license_.PcFqdns.Add(value);
                }
                else
                {
                    if (value.Length > 0)
                    {
                        license_.PcFqdns[0] = value;
                    }
                    else
                    {
                        license_.PcFqdns.Clear();
                    }
                }

                RaisePropertyChanged();
            }
        }
        public string Uuid
        {
            get { return license_.Uuid; }
            set { license_.Uuid = value; RaisePropertyChanged(); }
        }
        public ulong UseCount
        {
            get { return license_.UseCount; }
            set { license_.UseCount = value; RaisePropertyChanged(); }
        }

        public DateTime LicenseStart
        {
            get { return license_.LicenseStart; }
            set { license_.LicenseStart = value; RaisePropertyChanged(); }
        }
        public DateTime LicenseEnd
        {
            get { return license_.LicenseEnd; }
            set { license_.LicenseEnd = value; RaisePropertyChanged(); }
        }

        public bool LicenseValid
        {
            get { return license_.LicenseValid; }
            set { license_.LicenseValid = value; RaisePropertyChanged(); }
        }

        public string Issuer
        {
            get { return license_.Issuer; }
            set { license_.Issuer = value; RaisePropertyChanged(); }
        }
        public string LicenseComment
        {
            get { return license_.LicenseComment; }
            set { license_.LicenseComment = value; RaisePropertyChanged(); }
        }
    }

    public class HumbleUnlockResponseModel : BindableBase
    {
        private UnlockResponse response_;

        public UnlockResponse getEntity()
        {
            return response_;
        }

        public HumbleUnlockResponseModel(UnlockResponse response)
        {
            UpdateFromEntity(response);
        }

        public void UpdateFromEntity(UnlockResponse response)
        {
            response_ = response;
            specificLicenseModel_ = null;
            if (response_.Licenses.Count > 0)
            {
                specificLicenseModel_ = new HumbleSpecificLicenseModel(response_.Licenses[0]);
            }
            RaisePropertyChanged("SpecificLicense");
            RaisePropertyChanged("UnlockResponseVersion");
            RaisePropertyChanged("QstVersion");
            RaisePropertyChanged("PackageComment");
            RaisePropertyChanged("ServerFqdn");
        }

        private HumbleSpecificLicenseModel specificLicenseModel_;

        public HumbleSpecificLicenseModel SpecificLicense
        {
            get
            {
                if (response_.Licenses.Count == 0)
                {
                    response_.Licenses.Add(new SpecificLicense()
                    {
                        Uuid = System.Guid.NewGuid().ToString(), LicenseStart = DateTime.Today,
                        LicenseEnd = DateTime.Today, UseCount = 1, LicenseName = "EntryLight", LicenseValid = true
                    });
                    specificLicenseModel_ = new HumbleSpecificLicenseModel(response_.Licenses[0]);
                }
                return specificLicenseModel_;
            }
            set
            {
                if (response_.Licenses.Count == 0)
                {
                    response_.Licenses.Add(value.getEntity());
                }
                else
                {
                    response_.Licenses[0] = value.getEntity();
                }

                specificLicenseModel_ = value;
                RaisePropertyChanged();
            }
        }

        public ulong UnlockResponseVersion
        {
            get { return response_.UnlockResponseVersion; }
            set { response_.UnlockResponseVersion = value; RaisePropertyChanged(); }
        }
        public string QstVersion
        {
            get { return response_.QstVersion; }
            set { response_.QstVersion = value; RaisePropertyChanged(); }
        }
        public string PackageComment
        {
            get { return response_.PackageComment; }
            set { response_.PackageComment = value; RaisePropertyChanged(); }
        }

        public string ServerFqdn
        {
            get { return response_.ServerFqdn; }
            set { response_.ServerFqdn = value; RaisePropertyChanged(); }
        }
    }
}
