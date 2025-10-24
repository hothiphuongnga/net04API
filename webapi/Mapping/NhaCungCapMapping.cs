// DI hoa , 
// interface 
// lop thuc thi tuong ung

using webapi.Models;

namespace webapi.Mapping;

public interface INhaCungCapMapping
{
    // chuyển từ VM => Model
    NhaCungCap ToModel(NhaCungCapVM vm);
    // chuyển từ Model => VM
    NhaCungCapVM ToVM(NhaCungCap model);

}

public class NhaCungCapMapping : INhaCungCapMapping
{

    public NhaCungCap ToModel(NhaCungCapVM vm)
    {
        NhaCungCap model = new NhaCungCap();
        model.Id = vm.Id;
        model.Ten = vm.Ten;
        model.DiaChi = vm.DiaChi;
        //
        return model;
    }

    public NhaCungCapVM ToVM(NhaCungCap model)
    {
        NhaCungCapVM vm = new NhaCungCapVM();
        vm.Id = model.Id;
        vm.Ten = model.Ten;
        vm.DiaChi = model.DiaChi;
        //
        return vm;

    }

}