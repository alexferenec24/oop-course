using System;
using System.Collections.Generic;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class AnalyticsManager
    {
        private AppointmentManager _appointments;
        private DoctorManager _doctors;
        private PatientManager _patients;

        public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
        {
            _appointments = appointments;
            _doctors = doctors;
            _patients = patients;
        }

        public IEnumerable<DoctorStats> ComputeDoctorStats()
        {
            Doctor[] doctors = _doctors.GetAll();
            Appointment[] allAppointments = _appointments.GetAll();

            for (int i = 0; i < doctors.Length; i++)
            {
                Doctor doc = doctors[i];
                int count = 0;
                decimal revenue = 0m;
                DateTime lastDate = DateTime.MinValue;

                for (int j = 0; j < allAppointments.Length; j++)
                {
                    Appointment app = allAppointments[j];
                    if (app.DoctorId == doc.Id)
                    {
                        count++;
                        revenue += app.GetCost();
                        if (app.ScheduledAt > lastDate)
                        {
                            lastDate = app.ScheduledAt;
                        }
                    }
                }

                yield return new DoctorStats(doc.Id, doc.FullName, count, revenue, lastDate);
            }
        }

        public IEnumerable<PatientStats> ComputePatientStats()
        {
            Patient[] patients = _patients.GetAll();
            Appointment[] allAppointments = _appointments.GetAll();

            for (int i = 0; i < patients.Length; i++)
            {
                Patient pat = patients[i];
                int count = 0;
                decimal spent = 0m;
                DateTime lastDate = DateTime.MinValue;

                for (int j = 0; j < allAppointments.Length; j++)
                {
                    Appointment app = allAppointments[j];
                    if (app.PatientId == pat.Id)
                    {
                        count++;
                        spent += app.GetCost();
                        if (app.ScheduledAt > lastDate)
                        {
                            lastDate = app.ScheduledAt;
                        }
                    }
                }

                yield return new PatientStats(pat.Id, pat.FullName, count, spent, lastDate);
            }
        }
    }
}