using System;
using System.Collections.Generic;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointmentManager;

    public BillingManager(AppointmentManager appointmentManager)
    {
        _appointmentManager = appointmentManager;
    }

    public IPayable[] GetAllUnpaid()
    {
        return GetUnpaidFiltered(_appointmentManager.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return GetUnpaidFiltered(_appointmentManager.GetByPatient(patientId));
    }

    private IPayable[] GetUnpaidFiltered(Appointment[] source)
    {
        int count = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid && !source[i].IsCancelled)
            {
                count++;
            }
        }

        IPayable[] result = new IPayable[count];
        int index = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid && !source[i].IsCancelled)
            {
                result[index] = source[i];
                index++;
            }
        }

        return result;
    }

    public decimal GetTotalDebt()
    {
        decimal total = 0m;
        IPayable[] unpaid = GetAllUnpaid();
        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }
        return total;
    }

    public decimal GetPatientDebt(int patientId)
    {
        decimal total = 0m;
        IPayable[] unpaid = GetUnpaidByPatient(patientId);
        for (int i = 0; i < unpaid.Length; i++)
        {
            total += unpaid[i].GetCost();
        }
        return total;
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment[] all = _appointmentManager.GetAll();
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].Id == appointmentId)
            {
                if (!all[i].IsPaid && !all[i].IsCancelled)
                {
                    all[i].MarkPaid();
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            string details = "";
            if (items[i] is Appointment a)
            {
                details = a.ToString();
            }
            else
            {
                details = $"Запис #{i + 1}";
            }

            Console.WriteLine($"{details} | Сума: {items[i].GetCost().ToString("F2")} грн");
        }
    }
}