using System;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Logging;
using UnlockToolShared.Entities;
using UnlockToolShared.UseCases;

namespace Server.UseCases.UseCases
{
    public interface ILicenseUseCase
    {
        bool IsLicenseOk(string pcfqdn);
        bool IsLicenseFileLoaded();
    }

    public class LicenseUseCase : ILicenseUseCase
    {
        private IUnlockResponseReadDataAccess da_;
        private ILogger<LicenseUseCase> logger_;
        private UnlockResponse unlockResponse_;

        public LicenseUseCase(ILogger<LicenseUseCase> logger, IUnlockResponseReadDataAccess dataAccess)
        {
            logger_ = logger;
            da_ = dataAccess;
            LoadLicenseFile();
        }

        public bool IsLicenseFileLoaded()
        {
            return unlockResponse_ != null;
        }

        public bool LoadLicenseFile()
        {
            const string ServerLicenseFile = "ServerLicense.qst";
            try
            {
                UnlockResponse unlockResponse = null;
                var licenseFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerLicenseFile);
                using (var fs = File.OpenRead(licenseFile))
                {
                    unlockResponse = da_.ReadUnlockResponse(fs);
                }

                var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                var localfqdn = string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);
                if (localfqdn.EndsWith("."))
                {
                    localfqdn = localfqdn.Remove(localfqdn.Length - 1, 1);
                }

                if (unlockResponse.ServerFqdn.ToUpper() != localfqdn.ToUpper())
                {
                    logger_.LogCritical(string.Format("LicensFile was issued for {0}, but current is {1}",
                        unlockResponse.ServerFqdn, localfqdn));
                    return false;
                }

                unlockResponse_ = unlockResponse;
            }
            catch (Exception e)
            {
                logger_.LogCritical("Problem Loading License information", e);
                return false;
            }

            logger_.LogInformation("License Information loaded");

            return true;
        }

        public bool IsLicenseOk(string pcfqdn)
        {
            if (!IsLicenseFileLoaded())
            {
                return false;
            }

            if (unlockResponse_.Licenses.Count == 1)
            {
                if (unlockResponse_.Licenses[0].PcFqdns.Count > 0)
                {
                    bool found = false;
                    foreach (var fqdn in unlockResponse_.Licenses[0].PcFqdns)
                    {
                        found = fqdn.ToUpper() == pcfqdn.ToUpper();
                        if (found)
                        {
                            break;
                        }
                    }

                    if (!found)
                    {
                        logger_.LogCritical("License Invalid: No License in File for Pc: " + pcfqdn.ToUpper());
                        return false;
                    }

                    if (!unlockResponse_.Licenses[0].LicenseValid)
                    {
                        logger_.LogCritical("License Invalid: No Valid License found ");
                        return false;
                    }

                    DateTime now = DateTime.Now.Date;

                    if (!(now >= unlockResponse_.Licenses[0].LicenseStart &&
                          now <= unlockResponse_.Licenses[0].LicenseEnd))
                    {
                        logger_.LogCritical("License Invalid: No Valid License for current time found");
                        return false;
                    }
                }
            }
            else
            {
                logger_.LogCritical("License Invalid: No License in File");
                return false;
            }

            return true;
        }
    }
}
