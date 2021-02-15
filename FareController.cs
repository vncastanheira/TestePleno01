using System;
using System.Linq;
using TestePleno.Models;
using TestePleno.Services;

namespace TestePleno.Controllers
{
    public class FareController
    {
        private OperatorService _operatorService;
        private FareService FareService;

        public FareController()
        {
            _operatorService = new OperatorService();
            FareService = new FareService();
        }

        public void CreateFare(Fare fare, string operatorCode)
        {
            var selectedOperator = _operatorService.GetOperatorByCode(operatorCode);
            if (selectedOperator == null)
            {
                selectedOperator = new Operator { Id = Guid.NewGuid(), Code = operatorCode };
                _operatorService.Create(selectedOperator);
            }

            // check for any active fare in selected operator
            if (!FareService.GetFares()
                .Any(f => {
                    var span = DateTimeOffset.UtcNow - f.CreatedOn;
                    var months = Math.Floor(span.TotalDays / 30); // for simplicity

                    return f.OperatorId == selectedOperator.Id && f.Status == 1 && months <= 6;
                }))
            {
                fare.OperatorId = selectedOperator.Id;
                fare.Status = 1;
                fare.CreatedOn = DateTimeOffset.UtcNow;
                FareService.Create(fare);
            }
            else
            {
                throw new Exception($"Erro: não é possível cadastrar nova tarifa para {operatorCode}.\n"+
                                     "Motivo: { operatorCode } possui uma tarifa ativa.");
            }
        }
    }
}
