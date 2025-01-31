using System.Security.AccessControl;

namespace Emu.Common.Utils
{
    public class ParameterHelper
    {
        public static readonly string ResourceCategoryCompute = "Microsoft.Compute";

        public static readonly string ResourceTypeGallery = "galleries";
        public static readonly string ResourceTypeGalleryImage = "galleries/images";
        public static readonly string ResourceTypeVirtualMachine = "virtualMachines";

        public static string GetComputeResourceId(string subscriptionId, string resourceGroupName, params string[] additionalParams)
        {
            var allParams = new List<string> { "/subscriptions", subscriptionId, "resourceGroup", resourceGroupName };
            allParams.AddRange(additionalParams);

            // Join the parameters with "/"
            return string.Join("/", allParams);
        }
    }
}
