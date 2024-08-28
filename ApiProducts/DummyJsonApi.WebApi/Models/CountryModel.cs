namespace DummyJsonApi.WebApi;

public class CountryModel
{
    public string Currency { get; set; } = string.Empty;
    public int Discount { get; set; }
    public bool Reviews { get; set; }

}

/*
  Modelagem de dados: EFCore
  Id: deve ser init, porque depois de criar, não pode ser alterado.
  public int Id { get; set init; }
  --init
  --privete set
 -ValueGeneratedNever()
 -IEntityTypeConfiguration<>
 -HasConvertion
 --Modelagem de dominio

**/

