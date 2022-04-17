namespace YRM.Common.Entities
{
    public class AzureConfiguration
    {
        public const string KeyVaultUrlConst = "https://{0}.vault.azure.net/";

        public string? AzureDirectoryId { get; set; }
        public string? KeyVaultName { get; set; }
        public string? AzureAADClientId { get; set; }
        public string? AzureAADClientSecret { get; set; }

        public string GetKeyVaultUrl()
        {
            return string.Format(KeyVaultUrlConst, KeyVaultName);
        }
    }
}
