namespace BD_Labo01_oef02.Validators;

public class BrandValidator : AbstractValidator<Brand>
{
    public BrandValidator()
    {
        RuleFor(w => w.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(w => w.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(w => w.Logo).NotEmpty().WithMessage("Logo is required");
    }
}
