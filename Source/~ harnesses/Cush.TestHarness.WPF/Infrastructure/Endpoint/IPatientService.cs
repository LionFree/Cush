using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Cush.TestHarness.WPF.Model;

namespace Cush.TestHarness.WPF.Infrastructure.Endpoint
{
    [ServiceContract]
    public interface IPatientService
    {
        [OperationContract]
        [WebGet(UriTemplate = "patient/{id}", ResponseFormat = WebMessageFormat.Json)]
        Patient GetPatient(string id);

        [OperationContract]
        [WebGet(UriTemplate = "patients", ResponseFormat = WebMessageFormat.Json)]
        List<Patient> GetAllPatient();

        [OperationContract]
        [WebGet(UriTemplate = "patient?place={value}", ResponseFormat = WebMessageFormat.Json)]
        List<Patient> GetPatientByPlace(string value);

        [OperationContract]
        [WebInvoke(UriTemplate = "patient", Method = "POST")]
        int AddPatient(Patient patient);

        [OperationContract]
        [WebInvoke(UriTemplate = "patient/{id}", Method = "PUT")]
        bool UpdatePatient(string id, Patient patient);

        [OperationContract]
        [WebInvoke(UriTemplate = "patient/{id}", Method = "DELETE")]
        bool DeletePatient(string id);
    }
}
