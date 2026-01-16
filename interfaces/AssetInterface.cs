namespace ERP_BACKEND.interfaces;


using ERP_BACKEND.constracts;


    public interface IAsset
    {
        // Get all assets including their Category and Turbine info
        Task<IEnumerable<Asset>> GetAllAssetsAsync();

        // Get one specific asset by ID
        Task<Asset?> GetAssetByIdAsync(int id);

        // Add a new asset to the database0
        Task<Asset> AddAssetAsync(Asset asset);

        // // Update an existing asset
        // Task UpdateAssetAsync(Asset asset);

        // // Remove an asset
        // Task DeleteAssetAsync(int id);

        // // Check if an asset exists
        // Task<bool> AssetExistsAsync(int id);
    }
