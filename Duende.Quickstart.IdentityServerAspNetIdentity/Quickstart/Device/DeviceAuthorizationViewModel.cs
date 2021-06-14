// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.Quickstart.IdentityServerAspNetIdentity.Quickstart.Consent;

namespace Duende.Quickstart.IdentityServerAspNetIdentity.Quickstart.Device
{
    public class DeviceAuthorizationViewModel : ConsentViewModel
    {
        public string UserCode { get; set; }
        public bool ConfirmUserCode { get; set; }
    }
}