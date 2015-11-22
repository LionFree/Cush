using System.Collections.Generic;
using Cush.TestHarness.WPF.Model;

namespace Cush.TestHarness.WPF.Infrastructure.Endpoint
{
    public class PatientService : IPatientService 
    {
        public Patient GetPatient(string id)
        {
            return new Patient {Identifier = "CKALER", Name = "KALER, Curtis"};
            //try
            //{
            //    int patientId = Convert.ToInt32(id);

            //    using (var db = new PatientInformationEntities())
            //    {
            //        return db.Patients.SingleOrDefault(p => p.Id == patientId);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }

        public List<Patient> GetAllPatient()
        {
            return new List<Patient>();
            //try
            //{
            //    using (var db = new PatientInformationEntities())
            //    {
            //        return db.Patients.ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }

        public List<Patient> GetPatientByPlace(string value)
        {
            return new List<Patient>();
            //try
            //{
            //    using (var db = new PatientInformationEntities())
            //    {
            //        return db.Patients.Where(p => p.Place == value).ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }

        public int AddPatient(Patient patient)
        {
            return 0;
            //try
            //{
            //    using (var db = new PatientInformationEntities())
            //    {

            //        db.Patients.Add(patient);
            //        db.SaveChanges();

            //        return patient.Id;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }

        public bool UpdatePatient(string id, Patient patient)
        {
            return false;
            //try
            //{
            //    using (var db = new PatientInformationEntities())
            //    {

            //        Patient oldDetails = db.Patients.SingleOrDefault(p => p.Id == patient.Id);

            //        oldDetails.Name = patient.Name;
            //        oldDetails.DOB = patient.DOB;
            //        oldDetails.Place = patient.Place;

            //        db.SaveChanges();

            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }

        public bool DeletePatient(string id)
        {
            return false;
            //try
            //{

            //    int patientId = Convert.ToInt32(id);

            //    using (var db = new PatientInformationEntities())
            //    {

            //        Patient details = db.Patients.SingleOrDefault(p => p.Id == patientId);

            //        db.Patients.Remove(details);

            //        db.SaveChanges();

            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }
    }
}