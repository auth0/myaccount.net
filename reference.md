# Reference
## factors
<details><summary><code>client.Factors.<a href="/src/Auth0.MyAccountApi/Factors/FactorsClient.cs">ListAsync</a>() -> WithRawResponseTask&lt;ListFactorsResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

List of factors enabled for the Auth0 tenant and available for enrollment by this user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.Factors.ListAsync();
```
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## AuthenticationMethods
<details><summary><code>client.AuthenticationMethods.<a href="/src/Auth0.MyAccountApi/AuthenticationMethods/AuthenticationMethodsClient.cs">ListAsync</a>(ListAuthenticationMethodsRequestParameters { ... }) -> WithRawResponseTask&lt;ListAuthenticationMethodsResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve detailed list of authentication methods belonging to the authenticated user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.AuthenticationMethods.ListAsync(
    new ListAuthenticationMethodsRequestParameters { Type = FactorTypeEnum.Password }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListAuthenticationMethodsRequestParameters` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.AuthenticationMethods.<a href="/src/Auth0.MyAccountApi/AuthenticationMethods/AuthenticationMethodsClient.cs">CreateAsync</a>(CreateAuthenticationMethodRequestContent { ... }) -> WithRawResponseTask&lt;CreateAuthenticationMethodResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Start the enrollment of a supported authentication method.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.AuthenticationMethods.CreateAsync(
    new CreateAuthenticationMethodRequestContent(
        new CreateAuthenticationMethodRequestContent.Phone(
            new CreatePhoneAuthenticationMethod { PhoneNumber = "phone_number" }
        )
    )
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateAuthenticationMethodRequestContent` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.AuthenticationMethods.<a href="/src/Auth0.MyAccountApi/AuthenticationMethods/AuthenticationMethodsClient.cs">GetAsync</a>(authenticationMethodId) -> WithRawResponseTask&lt;GetAuthenticationMethodResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieves a single authentication method belonging to the authenticated user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.AuthenticationMethods.GetAsync("authentication_method_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**authenticationMethodId:** `string` — Authentication Method ID. This value is part of the Location header returned when creating an authentication method. It should be used as it is, without any modifications.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.AuthenticationMethods.<a href="/src/Auth0.MyAccountApi/AuthenticationMethods/AuthenticationMethodsClient.cs">DeleteAsync</a>(authenticationMethodId) -> WithRawResponseTask</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Deletes a single authentication method belonging to the authenticated user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.AuthenticationMethods.DeleteAsync("authentication_method_id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**authenticationMethodId:** `string` — Authentication Method ID. This value is part of the Location header returned when creating an authentication method. It should be used as it is, without any modifications.
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.AuthenticationMethods.<a href="/src/Auth0.MyAccountApi/AuthenticationMethods/AuthenticationMethodsClient.cs">UpdateAsync</a>(authenticationMethodId, UpdateAuthenticationMethodRequestContent { ... }) -> WithRawResponseTask&lt;UpdateAuthenticationMethodResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Updates a single authentication method
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.AuthenticationMethods.UpdateAsync(
    "authentication_method_id",
    new UpdateAuthenticationMethodRequestContent()
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**authenticationMethodId:** `string` — Authentication Method ID. This value is part of the Location header returned when creating an authentication method. It should be used as it is, without any modifications.
    
</dd>
</dl>

<dl>
<dd>

**request:** `UpdateAuthenticationMethodRequestContent` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.AuthenticationMethods.<a href="/src/Auth0.MyAccountApi/AuthenticationMethods/AuthenticationMethodsClient.cs">VerifyAsync</a>(authenticationMethodId, VerifyAuthenticationMethodRequestContent { ... }) -> WithRawResponseTask&lt;VerifyAuthenticationMethodResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Confirm the enrollment of a supported authentication method.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.AuthenticationMethods.VerifyAsync(
    "authentication_method_id",
    new VerifyEmailAuthenticationMethod { AuthSession = "Fe26.2**05c400ed...", OtpCode = "123456" }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**authenticationMethodId:** `string` — Authentication Method ID. This value is part of the Location header returned when creating an authentication method. It should be used as it is, without any modifications.
    
</dd>
</dl>

<dl>
<dd>

**request:** `VerifyAuthenticationMethodRequestContent` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## ConnectedAccounts
<details><summary><code>client.ConnectedAccounts.<a href="/src/Auth0.MyAccountApi/ConnectedAccounts/ConnectedAccountsClient.cs">CreateAsync</a>(CreateConnectedAccountsRequestContent { ... }) -> WithRawResponseTask&lt;CreateConnectedAccountsResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Start an authorization flow to link the authenticated user's account with an external identity provider.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ConnectedAccounts.CreateAsync(
    new CreateConnectedAccountsRequestContent
    {
        Connection = "connection",
        RedirectUri = "redirect_uri",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CreateConnectedAccountsRequestContent` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ConnectedAccounts.<a href="/src/Auth0.MyAccountApi/ConnectedAccounts/ConnectedAccountsClient.cs">CompleteAsync</a>(CompleteConnectedAccountsRequestContent { ... }) -> WithRawResponseTask&lt;CompleteConnectedAccountsResponseContent&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Complete a previously started authorization flow to link the authenticated user's account with an external identity provider.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ConnectedAccounts.CompleteAsync(
    new CompleteConnectedAccountsRequestContent
    {
        AuthSession = "auth_session",
        ConnectCode = "connect_code",
        RedirectUri = "redirect_uri",
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `CompleteConnectedAccountsRequestContent` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ConnectedAccounts.<a href="/src/Auth0.MyAccountApi/ConnectedAccounts/ConnectedAccountsClient.cs">ListAsync</a>(ListConnectedAccountsRequestParameters { ... }) -> Pager&lt;ConnectedAccount&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve connected accounts belonging to the authenticated user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ConnectedAccounts.ListAsync(
    new ListConnectedAccountsRequestParameters
    {
        Connection = "connection",
        From = "from",
        Take = 1,
    }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListConnectedAccountsRequestParameters` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

<details><summary><code>client.ConnectedAccounts.<a href="/src/Auth0.MyAccountApi/ConnectedAccounts/ConnectedAccountsClient.cs">DeleteAsync</a>(id) -> WithRawResponseTask</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Delete a connected account belonging to the authenticated user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ConnectedAccounts.DeleteAsync("id");
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**id:** `string` — The unique identifier of the connected account
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

## ConnectedAccounts Connections
<details><summary><code>client.ConnectedAccounts.Connections.<a href="/src/Auth0.MyAccountApi/ConnectedAccounts/Connections/ConnectionsClient.cs">ListAsync</a>(ListConnectedAccountsConnectionsRequestParameters { ... }) -> Pager&lt;ConnectedAccountConnection&gt;</code></summary>
<dl>
<dd>

#### 📝 Description

<dl>
<dd>

<dl>
<dd>

Retrieve available connections that can be used for account linking by the authenticated user.
</dd>
</dl>
</dd>
</dl>

#### 🔌 Usage

<dl>
<dd>

<dl>
<dd>

```csharp
await client.ConnectedAccounts.Connections.ListAsync(
    new ListConnectedAccountsConnectionsRequestParameters { From = "from", Take = 1 }
);
```
</dd>
</dl>
</dd>
</dl>

#### ⚙️ Parameters

<dl>
<dd>

<dl>
<dd>

**request:** `ListConnectedAccountsConnectionsRequestParameters` 
    
</dd>
</dl>
</dd>
</dl>


</dd>
</dl>
</details>

