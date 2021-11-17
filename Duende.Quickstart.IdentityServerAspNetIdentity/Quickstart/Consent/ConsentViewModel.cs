﻿// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace Duende.Quickstart.IdentityServerAspNetIdentity.Quickstart.Consent;

public class ConsentViewModel : ConsentInputModel
{
    public string ClientName { get; set; }
    public string ClientUrl { get; set; }
    public string ClientLogoUrl { get; set; }
    public bool AllowRememberConsent { get; set; }

    public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
    public IEnumerable<ScopeViewModel> ApiScopes { get; set; }
}