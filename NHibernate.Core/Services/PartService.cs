using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Specifications.PartSpecifications;

namespace NHibernatePoc.Core.Services
{
    public class PartService :Service<Part>, IPartService
    {
        private readonly IValidationService _validationService;

        public PartService(IPartRepository partRepository, IValidationService validationService): base(partRepository) 
        {
            _validationService = validationService;
        }

        public override void Add(Part part)
        {
            _validationService.ValidatePart(part);
            _repository.Add(part);
        }

        public override void Update(Part part)
        {
            _validationService.ValidatePart(part);
            _repository.Update(part);
        }

        public override void Delete(int id)
        {
            var part = _repository.GetById(id);
            if (part == null) throw new PartNotFoundException(id);
            _repository.Remove(part);
        }

        public IEnumerable<Part> GetPartsByCondition(PartCondition condition)
        {
            var specification = new PartConditionSpecification(condition);
            return _repository.FindBySpecification(specification.ToExpression());
        }
    }
}
