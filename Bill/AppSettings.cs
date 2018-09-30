using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill
{
    public class AppSettings
    {
      private const string SecretAuthenticationKey = "yVn7LajtZQ+c22xige+tJBIvt/ypwvAhlPznDDS6d9lmO9PdjgBYuYo34EGCmrLRSMHOIR8CKI7VLuNLl8sQ14ZOaOKwsRc+yi+D2VbElBWEM+PvMcXGQTJbtiwH5Iw5kHNdhgEBf2knkt7CsMUNMkREP9G+Of0ObU9onC6Fb2Q4G5+7jPhGCyFhFhE0VjKIpjf26BucWMWJu5K8UNO5S3P44kvUFxbU8Yt9T9/mBGEiRVSeW+UcNKOKIgGPlRG4hrVYHt2BPANcr7AJ/ilxOWhfnmyqPrtt2OnT6Y49UUd/9pzLynkSSsCXJ4PRAA37xTSoQ3Jt9+YUII1++nCHgQ==";
      public static SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretAuthenticationKey));
      public static string Url = "http://localhost:58562";
      public static string Issuer = "http://localhost:58562";
    }
}
