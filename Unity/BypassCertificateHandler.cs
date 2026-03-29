using UnityEngine.Networking;

/// <summary>
/// Bypasses SSL certificate validation for self-signed certs (dev/LAN only).
/// Attach to any UnityWebRequest via: request.certificateHandler = new BypassCertificateHandler();
/// DO NOT use this in production builds — replace with a proper cert or pinning.
/// </summary>
public class BypassCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
