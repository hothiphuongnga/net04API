using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using webapi.Models;
using webapi.ViewModels;

public class AutoMapperProfile : Profile
{

    public AutoMapperProfile()
    {
        // KhachHang => KhachHangVM
        CreateMap<KhachHang, KhachHangVM>();
        // CreateMap<KhachHang, KhachHangVM>();
        // .ForMember(modelVm => modelVm.MetaData,
        //                         m => m.MapFrom(entity => JsonSerializer.Deserialize<List<string>>(entity.MetaData)));


        // KhachHangVM => KhachHang
        CreateMap<KhachHangVM, KhachHang>();





    }
    /*
    KhachHang y chang DB : metadata : string

    KhachHangVM : metadata : List<string>




    */
}