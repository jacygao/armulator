using System.Security.AccessControl;

namespace Emu.Common.Utils
{
    public class ParameterHelper
    {
        public static readonly string ResourceCategoryCompute = "Microsoft.Compute";

        public static readonly string ResourceTypeGallery = "galleries";
        public static readonly string ResourceTypeGalleryImage = "galleries/images";
        public static readonly string ResourceTypeVirtualMachine = "virtualMachines";

        public static string GetComputeResourceId(string subscriptionId, string resourceGroupName, string resourceType, string resourceName)
        {
            return $"/subscriptions/{subscriptionId}/resourceGroup/{resourceGroupName}/providers/Microsoft.Compute/{resourceType}/{resourceName}";
        }
    }
}
