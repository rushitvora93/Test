using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;

namespace FrameworksAndDrivers.CertificateStoreAccess
{
    public class HumbleCertifcateHandler
    {
        private readonly ILogger logger_;
        private readonly StoreName serverCertStoreName = StoreName.My;
        private readonly StoreLocation serverCertStoreLocation = StoreLocation.LocalMachine;

        private readonly StoreName caCertStoreName = StoreName.Root;
        private readonly StoreLocation caCertStoreLocation = StoreLocation.LocalMachine;

        public HumbleCertifcateHandler(ILogger logger)
        {
            logger_ = logger;
        }

        private bool AddCertificate(string filepath, string password, StoreName storeName, StoreLocation storeLocation)
        {
            logger_.LogInformation("try adding certificate to " + storeLocation.ToString() + " / " +
                                   storeName.ToString());
            try
            {
                using X509Certificate2 cert =
                    new X509Certificate2(filepath, password, X509KeyStorageFlags.MachineKeySet);
                {
                    using X509Store store = new X509Store(storeName, storeLocation);
                    {
                        store.Open(OpenFlags.ReadWrite);
                        store.Add(cert);
                        logger_.LogInformation("done adding certificate to " + storeLocation.ToString() + " / " +
                                               storeName.ToString());
                        return true;
                    }
                }

            }
            catch (Exception e)
            {
                logger_.LogError(
                    "Error by adding certificate to " + storeLocation.ToString() + " / " + storeName.ToString(), e);
                return false;
            }
        }

        public bool AddCaCertificateToLocalMachine(string filepath)
        {
            return AddCertificate(filepath, "", caCertStoreName, caCertStoreLocation);
        }

        public bool AddServerCertificateToLocalMachine(string filepath, string password)
        {
            return AddCertificate(filepath, password, serverCertStoreName, serverCertStoreLocation);
        }

        private bool RemoveCertificate(string thumbprint, StoreName storeName, StoreLocation storeLocation)
        {
            logger_.LogInformation("try removing certificate from " + storeLocation.ToString() + " / " +
                                   storeName.ToString());
            try
            {
                using X509Store store = new X509Store(storeName, storeLocation);
                {
                    store.Open(OpenFlags.ReadWrite);
                    var cert = FindCertificateByThumbPrint(thumbprint, storeName, storeLocation);
                    if (cert == null)
                    {
                        return false;
                    }
                    store.Remove(cert);
                    logger_.LogInformation("certificate removed: " + cert.ToString());
                    logger_.LogInformation("done removing certificate");
                    return true;
                }
            }
            catch (Exception e)
            {
                logger_.LogError(
                    "Error by removing certificate from " + storeLocation.ToString() + " / " + storeName.ToString(), e);
                return false;
            }
        }

        public bool RemoveCaCertificateFromLocalMachine(string thumbprint)
        {
            return RemoveCertificate(thumbprint, caCertStoreName, caCertStoreLocation);
        }

        public bool RemoveServerCertificateFromLocalMachine(string thumbprint)
        {
            return RemoveCertificate(thumbprint, serverCertStoreName, serverCertStoreLocation);
        }

        public X509Certificate2 FindServerCertificateByThumbPrint(string thumbprint)
        {
            return FindCertificateByThumbPrint(thumbprint, serverCertStoreName, serverCertStoreLocation);
        }

        private X509Certificate2 FindCertificateByThumbPrint(string thumbprint, StoreName storeName, StoreLocation storeLocation )
        {
            using X509Store store = new X509Store(storeName, storeLocation);
            {
                store.Open(OpenFlags.ReadOnly);
                var certcollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
                if (certcollection.Count == 0)
                {
                    logger_.LogCritical("certificate not found: " + thumbprint);
                    return null;
                }

                if (certcollection.Count > 1)
                {
                    logger_.LogWarning("more then one certificate found, using first one: " + certcollection[0].ToString());
                }

                logger_.LogInformation("certificate found: " + certcollection[0].ToString());

                return certcollection[0];
            }
        }

    }
}
