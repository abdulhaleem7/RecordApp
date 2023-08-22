using System;

namespace Application.DTOs
{
    public class SelectListItemData
    {
        public SelectListItemData(Guid id,
                                  string code,
                                  string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }



        public Guid Id { get; }
        public string Code { get; }
        public string Name { get; }
    }


    public class SelectCustomerListItemData
    {
        public SelectCustomerListItemData(string id,
                                  string code,
                                  string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }



        public string Id { get; }
        public string Code { get; }
        public string Name { get; }
    }
    public class SelectOtherListItemData
    {
        public SelectOtherListItemData(string id,
                                  string code,
                                  string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }



        public string Id { get; }
        public string Code { get; }
        public string Name { get; }
    }
}
