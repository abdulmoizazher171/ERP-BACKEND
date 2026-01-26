using AutoMapper;
using ERP_BACKEND.mappers;
using ERP_BACKEND.constracts;

namespace ERP_BACKEND.data;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AssetPlacement, PlacementMapper>()


        // Map the ID (Check your Entity for the exact property name)
            .ForMember(dest => dest.PlacementId, opt => opt.MapFrom(src => src.PLACEMENT_ID))
            
            // Map the Date
            .ForMember(dest => dest.PlacedDate, opt => opt.MapFrom(src => src.PLACED_DATE))
            
            // Map the Person
            .ForMember(dest => dest.PlacedBy, opt => opt.MapFrom(src => src.PLACED_BY))

            .ForMember(dest => dest.WithdrawnBy, opt => opt.MapFrom(src => src.WITHDRAWN_BY))

            .ForMember(dest => dest.WithdrawalDate, opt => opt.MapFrom(src => src.WITHDRAWAL_DATE))

            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LOCATION))




            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QUANTITY))

            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Asset.Category.CATEGORY_NAME))

            .ForMember(dest => dest.SystemNumber, opt => opt.MapFrom(src => src.Asset.Turbine.SYSTEM_NUMBER))
            
            
            .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Asset.ITEM_NAME))
            // Map RackNumber from the nested Rack object
            .ForMember(dest => dest.RackNumber, opt => opt.MapFrom(src => src.Rack.RACK_NUMBER))
            // Map ShelfName from the nested Shelf object
            .ForMember(dest => dest.ShelfName, opt => opt.MapFrom(src => src.Shelf.SHELF_NAME));
    }
}