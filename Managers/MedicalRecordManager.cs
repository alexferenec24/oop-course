using System;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class MedicalRecordManager
    {
        private const int MaxRecords = 1000;
        private MedicalRecord[] _records = new MedicalRecord[MaxRecords];
        private int _count = 0;

        public int Count => _count;

        public void Add(MedicalRecord record)
        {
            if (_count < MaxRecords)
            {
                _records[_count] = record;
                _count++;
                Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
            }
            else
            {
                Console.WriteLine("Досягнуто ліміт записів (1000).");
            }
        }

        public MedicalRecord? FindById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].Id == id)
                {
                    return _records[i];
                }
            }
            return null;
        }

        public MedicalRecord[] GetByPatient(int patientId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId) matches++;
            }

            MedicalRecord[] result = new MedicalRecord[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId)
                {
                    result[index] = _records[i];
                    index++;
                }
            }
            return result;
        }

        public MedicalRecord[] GetByDoctor(int doctorId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].DoctorId == doctorId) matches++;
            }

            MedicalRecord[] result = new MedicalRecord[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].DoctorId == doctorId)
                {
                    result[index] = _records[i];
                    index++;
                }
            }
            return result;
        }

        // ============================================================
        // ЗАДАЧА 3: фільтрація за реальним типом (is / as)
        // ============================================================

        // 1. Усі діагнози пацієнта
        public Diagnosis[] GetDiagnoses(int patientId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Diagnosis)
                {
                    matches++;
                }
            }

            Diagnosis[] result = new Diagnosis[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Diagnosis d)
                {
                    result[index] = d;
                    index++;
                }
            }
            return result;
        }

        // 2. Усі аналізи пацієнта
        public LabResult[] GetLabResults(int patientId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is LabResult)
                {
                    matches++;
                }
            }

            LabResult[] result = new LabResult[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is LabResult lr)
                {
                    result[index] = lr;
                    index++;
                }
            }
            return result;
        }

        // 3. Усі рецепти пацієнта
        public Prescription[] GetPrescriptions(int patientId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Prescription)
                {
                    matches++;
                }
            }

            Prescription[] result = new Prescription[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId && _records[i] is Prescription p)
                {
                    result[index] = p;
                    index++;
                }
            }
            return result;
        }

        // 4. Лише хронічні діагнози
        public Diagnosis[] GetChronicDiagnoses(int patientId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId
                    && _records[i] is Diagnosis d
                    && d.IsChronic)
                {
                    matches++;
                }
            }

            Diagnosis[] result = new Diagnosis[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId
                    && _records[i] is Diagnosis d
                    && d.IsChronic)
                {
                    result[index] = d;
                    index++;
                }
            }
            return result;
        }

        // 5. Лише активні рецепти
        public Prescription[] GetActivePrescriptions(int patientId)
        {
            int matches = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId
                    && _records[i] is Prescription p
                    && p.IsActive())
                {
                    matches++;
                }
            }

            Prescription[] result = new Prescription[matches];
            int index = 0;
            for (int i = 0; i < _count; i++)
            {
                if (_records[i].PatientId == patientId
                    && _records[i] is Prescription p
                    && p.IsActive())
                {
                    result[index] = p;
                    index++;
                }
            }
            return result;
        }

        // 6. Зведена картка пацієнта
        public void DisplayPatientSummary(int patientId)
        {
            Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");

            MedicalRecord[] all = GetByPatient(patientId);
            if (all.Length == 0)
            {
                Console.WriteLine("Записів немає.");
                return;
            }

            Diagnosis[] diagnoses = GetDiagnoses(patientId);
            LabResult[] labs = GetLabResults(patientId);
            Prescription[] prescriptions = GetPrescriptions(patientId);

            Console.WriteLine($"Всього записів: {all.Length} "
                            + $"(діагнозів: {diagnoses.Length}, "
                            + $"аналізів: {labs.Length}, "
                            + $"рецептів: {prescriptions.Length})");

            Diagnosis[] chronic = GetChronicDiagnoses(patientId);
            if (chronic.Length > 0)
            {
                Console.WriteLine($"Хронічні діагнози ({chronic.Length}):");
                for (int i = 0; i < chronic.Length; i++)
                {
                    Console.WriteLine($"  {chronic[i]}");
                }
            }

            Prescription[] active = GetActivePrescriptions(patientId);
            if (active.Length > 0)
            {
                Console.WriteLine($"Активні рецепти ({active.Length}):");
                for (int i = 0; i < active.Length; i++)
                {
                    Console.WriteLine($"  {active[i]} | до {active[i].ExpiresAt:dd.MM.yyyy}");
                }
            }
        }

        // ============================================================

        public void DisplayAll()
        {
            if (_count == 0)
            {
                Console.WriteLine("Записів немає.");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_records[i].ToString());
            }
        }

        public void DisplayList(MedicalRecord[] list)
        {
            if (list.Length == 0)
            {
                Console.WriteLine("не знайдено");
                return;
            }

            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i].ToString());
            }
        }

        public MedicalRecord? this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) return null;
                return _records[index];
            }
        }
    }
}