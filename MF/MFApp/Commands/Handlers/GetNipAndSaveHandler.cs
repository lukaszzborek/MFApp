using MediatR;
using MFApp.Clients;
using MFApp.EF;
using MFApp.Exceptions;
using MFApp.Model;
using MFApp.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace MFApp.Commands.Handlers;

public class GetNipAndSaveHandler : IRequestHandler<GetNipAndSave, NipData>
{
    private readonly MFDbContext _dbContext;
    private readonly IMFClient _mfClient;

    public GetNipAndSaveHandler(IMFClient mfClient, MFDbContext dbContext)
    {
        _mfClient = mfClient;
        _dbContext = dbContext;
    }

    public async Task<NipData> Handle(GetNipAndSave request, CancellationToken cancellationToken)
    {
        var createNew = false;
        var nip = await _dbContext
                        .Nips
                        .Include(x => x.AccountNumbers)
                        .Include(x => x.Representatives)
                        .Include(x => x.Partners)
                        .Include(x => x.AuthorizedClerks)
                        .Where(x => x.Nip == request.Nip)
                        .FirstOrDefaultAsync(cancellationToken);

        if (nip == null || DateOnly.FromDateTime(nip.LastUpdateDate) != request.Date)
        {
            var nipResult = await _mfClient.GetNipResultAsync(request.Nip, request.Date);
            if (!nipResult.Success && nip == null)
                throw new FailedToGetNipDataException(nipResult.ErrorMessage);

            var nipDto = nipResult.Result.Subject;
            if (nip == null)
            {
                nip = new NipData();
                nip.Nip = nipDto.Nip;
                _dbContext.Nips.Add(nip);
                createNew = true;
            }

            nip.Name = nipDto.Name;
            nip.Regon = nipDto.Regon;
            nip.Krs = nipDto.Krs;
            nip.ResidenceAddress = nipDto.ResidenceAddress;
            nip.WorkingAddress = nipDto.WorkingAddress;
            nip.HasVirtualAccounts = nipDto.HasVirtualAccounts;
            nip.StatusVat = nipDto.StatusVat;
            nip.RestorationBasis = nipDto.RestorationBasis;
            nip.RestorationDate = nipDto.RestorationDate;
            nip.RemovalBasis = nipDto.RemovalBasis;
            nip.RemovalDate = nipDto.RemovalDate;
            nip.RegistrationDenialBasis = nipDto.RegistrationDenialBasis;
            nip.RegistrationDenialDate = nipDto.RegistrationDenialDate;
            nip.RegistrationLegalDate = nipDto.RegistrationLegalDate;
            nip.Pesel = nipDto.Pesel;
            nip.AccountNumbers.Clear();
            nip.AccountNumbers.AddRange(
                nipDto.AccountNumbers.Select(x => new NipAccountNumber { Nip = nip.Nip, Number = x }).ToList());
            nip.Partners.Clear();

            var partners = nipDto
                           .Partners
                           .Select(x => new Partner
                           {
                               Nip = nip.Nip, Type = RepresentativeType.Partner, CompanyName = x.CompanyName,
                               FirstName = x.FirstName, LastName = x.LastName, Pesel = x.Pesel
                           }).ToList();
            nip.Partners.AddRange(partners);

            nip.Representatives.Clear();
            var representatives = nipDto
                                  .Representatives
                                  .Select(x => new Representative
                                  {
                                      Nip = nip.Nip, Type = RepresentativeType.Representative,
                                      CompanyName = x.CompanyName,
                                      FirstName = x.FirstName, LastName = x.LastName, Pesel = x.Pesel
                                  }).ToList();
            nip.Representatives.AddRange(representatives);

            nip.AuthorizedClerks.Clear();
            var authorizedClerks = nipDto
                                   .AuthorizedClerks
                                   .Select(x => new AuthorizedClerk
                                   {
                                       Nip = nip.Nip, Type = RepresentativeType.AuthorizedClerk,
                                       CompanyName = x.CompanyName,
                                       FirstName = x.FirstName, LastName = x.LastName, Pesel = x.Pesel
                                   }).ToList();
            nip.AuthorizedClerks.AddRange(authorizedClerks);


            nip.LastUpdateDate = DateTime.UtcNow;
            if (!createNew)
                _dbContext.Nips.Update(nip);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return nip;
    }
}