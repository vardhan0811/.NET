using System;
using System.Collections.Generic;
using System.Linq;

namespace Day39
{
    // ==========================================
    // Base Interfaces & Enums
    // ==========================================
    public interface IPatient
    {
        int PatientId { get; }
        string? Name { get; }
        DateTime DateOfBirth { get; }
        BloodType BloodType { get; }
    }

    public enum BloodType { A, B, AB, O }

    // ==========================================
    // Generic Priority Queue
    // ==========================================
    public class PriorityQueue<T> where T : IPatient
    {
        private SortedDictionary<int, Queue<T>> _priorityQueues = new();

        public void Enqueue(T patient, int priorityLevel)
        {
            if (priorityLevel < 1 || priorityLevel > 5)
                throw new ArgumentException("Priority must be between 1 and 5");

            if (!_priorityQueues.ContainsKey(priorityLevel))
                _priorityQueues[priorityLevel] = new Queue<T>();

            _priorityQueues[priorityLevel].Enqueue(patient);
        }

        public T Dequeue()
        {
            foreach (var queue in _priorityQueues)
            {
                if (queue.Value.Count > 0)
                    return queue.Value.Dequeue();
            }

            throw new InvalidOperationException("No patients in queue.");
        }
    }

    // ==========================================
    // Generic Medication System
    // ==========================================
    public class MedicationSystem<T> where T : IPatient
    {
        private Dictionary<T, List<(string medication, DateTime time)>> _medicationHistory = new();

        public void PrescribeMedication(
            T patient,
            string medicationName,
            Func<T, bool> dosageValidator)
        {
            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }

            if (!dosageValidator(patient))
            {
                Console.WriteLine("Dosage validation failed.");
                return;
            }

            if (!_medicationHistory.ContainsKey(patient))
                _medicationHistory[patient] = new List<(string, DateTime)>();

            _medicationHistory[patient].Add((medicationName, DateTime.Now));

            Console.WriteLine("Medication prescribed successfully.");
        }
    }

    // ==========================================
    // Specialized Patients
    // ==========================================
    public class PediatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public string? GuardianName { get; set; }
        public double Weight { get; set; }
    }

    public class GeriatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string? Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public List<string> ChronicConditions { get; } = new();
        public int MobilityScore { get; set; }
    }

    // ==========================================
    // Hospital Management System
    // ==========================================
    public class HospitalPatientManagementSystem
    {
        public static void Run()
        {
            var pediatricPatients = new List<PediatricPatient>();
            var geriatricPatients = new List<GeriatricPatient>();

            var pediatricQueue = new PriorityQueue<PediatricPatient>();
            var geriatricQueue = new PriorityQueue<GeriatricPatient>();

            var pediatricMedicationSystem = new MedicationSystem<PediatricPatient>();
            var geriatricMedicationSystem = new MedicationSystem<GeriatricPatient>();

            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.WriteLine("\n====== HOSPITAL PATIENT MANAGEMENT SYSTEM ======");
                Console.WriteLine("1. Register Pediatric Patient");
                Console.WriteLine("2. Register Geriatric Patient");
                Console.WriteLine("3. Add Patient to Priority Queue");
                Console.WriteLine("4. Process Next Patient");
                Console.WriteLine("5. Prescribe Medication");
                Console.WriteLine("6. Exit");

                Console.Write("Select an option: ");
                string selectedOption = Console.ReadLine() ?? string.Empty;

                try
                {
                    switch (selectedOption)
                    {
                        case "1":

                            Console.Write("Enter Patient ID: ");
                            string? pediatricIdInput = Console.ReadLine();
                            if (!int.TryParse(pediatricIdInput, out int pediatricId))
                            {
                                Console.WriteLine("Invalid Patient ID.");
                                break;
                            }

                            Console.Write("Enter Patient Name: ");
                            string pediatricName = Console.ReadLine() ?? string.Empty;

                            Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                            string? pediatricDobInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(pediatricDobInput))
                            {
                                Console.WriteLine("Invalid Date of Birth.");
                                break;
                            }
                            DateTime pediatricDob = DateTime.Parse(pediatricDobInput);

                            Console.Write("Enter Weight (kg): ");
                            string? pediatricWeightInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(pediatricWeightInput) || !double.TryParse(pediatricWeightInput, out double pediatricWeight))
                            {
                                Console.WriteLine("Invalid Weight.");
                                break;
                            }

                            var pediatricPatient = new PediatricPatient
                            {
                                PatientId = pediatricId,
                                Name = pediatricName,
                                DateOfBirth = pediatricDob,
                                Weight = pediatricWeight
                            };

                            pediatricPatients.Add(pediatricPatient);
                            Console.WriteLine("Pediatric patient registered successfully.");
                            break;

                        case "2":

                            Console.Write("Enter Patient ID: ");
                            string? geriatricIdInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(geriatricIdInput) || !int.TryParse(geriatricIdInput, out int geriatricId))
                            {
                                Console.WriteLine("Invalid Patient ID.");
                                break;
                            }

                            Console.Write("Enter Patient Name: ");
                            string geriatricName = Console.ReadLine() ?? string.Empty;

                            Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                            string? geriatricDobInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(geriatricDobInput) || !DateTime.TryParse(geriatricDobInput, out DateTime geriatricDob))
                            {
                                Console.WriteLine("Invalid Date of Birth.");
                                break;
                            }

                            var geriatricPatient = new GeriatricPatient
                            {
                                PatientId = geriatricId,
                                Name = geriatricName,
                                DateOfBirth = geriatricDob
                            };

                            geriatricPatients.Add(geriatricPatient);
                            Console.WriteLine("Geriatric patient registered successfully.");
                            break;

                        case "3":

                            Console.WriteLine("\nSelect Patient Type:");
                            Console.WriteLine("1 - Pediatric");
                            Console.WriteLine("2 - Geriatric");
                            Console.Write("Enter Type: ");
                            string? queueTypeInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(queueTypeInput) || !int.TryParse(queueTypeInput, out int queueType))
                            {
                                Console.WriteLine("Invalid patient type.");
                                break;
                            }

                            Console.Write("Enter Patient ID: ");
                            string? queuePatientIdInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(queuePatientIdInput) || !int.TryParse(queuePatientIdInput, out int queuePatientId))
                            {
                                Console.WriteLine("Invalid Patient ID.");
                                break;
                            }

                            Console.Write("Enter Priority Level (1-5): ");
                            string? priorityInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(priorityInput) || !int.TryParse(priorityInput, out int priorityLevel))
                            {
                                Console.WriteLine("Invalid Priority Level.");
                                break;
                            }

                            if (queueType == 1)
                            {
                                var selectedPediatric =
                                    pediatricPatients.FirstOrDefault(p => p.PatientId == queuePatientId);

                                if (selectedPediatric != null)
                                {
                                    pediatricQueue.Enqueue(selectedPediatric, priorityLevel);
                                    Console.WriteLine("Pediatric patient added to queue.");
                                }
                                else
                                {
                                    Console.WriteLine("Pediatric patient not found.");
                                }
                            }
                            else if (queueType == 2)
                            {
                                var selectedGeriatric =
                                    geriatricPatients.FirstOrDefault(p => p.PatientId == queuePatientId);

                                if (selectedGeriatric != null)
                                {
                                    geriatricQueue.Enqueue(selectedGeriatric, priorityLevel);
                                    Console.WriteLine("Geriatric patient added to queue.");
                                }
                                else
                                {
                                    Console.WriteLine("Geriatric patient not found.");
                                }
                            }
                            break;

                        case "4":

                            Console.WriteLine("\nSelect Queue Type:");
                            Console.WriteLine("1 - Pediatric");
                            Console.WriteLine("2 - Geriatric");
                            Console.Write("Enter Type: ");
                            string? processTypeInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(processTypeInput) || !int.TryParse(processTypeInput, out int processType))
                            {
                                Console.WriteLine("Invalid type selected.");
                                break;
                            }

                            if (processType == 1)
                                Console.WriteLine($"Processing Pediatric Patient: {pediatricQueue.Dequeue().Name}");
                            else if (processType == 2)
                                Console.WriteLine($"Processing Geriatric Patient: {geriatricQueue.Dequeue().Name}");
                            else
                                Console.WriteLine("Invalid type selected.");

                            break;

                        case "5":

                            Console.WriteLine("\nSelect Patient Type:");
                            Console.WriteLine("1 - Pediatric");
                            Console.WriteLine("2 - Geriatric");
                            Console.Write("Enter Type: ");
                            string? medicationTypeInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(medicationTypeInput) || !int.TryParse(medicationTypeInput, out int medicationType))
                            {
                                Console.WriteLine("Invalid patient type.");
                                break;
                            }

                            Console.Write("Enter Patient ID: ");
                            string? medicationPatientIdInput = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(medicationPatientIdInput) || !int.TryParse(medicationPatientIdInput, out int medicationPatientId))
                            {
                                Console.WriteLine("Invalid Patient ID.");
                                break;
                            }

                            Console.Write("Enter Medication Name: ");
                            string medicationName = Console.ReadLine() ?? string.Empty;

                            if (medicationType == 1)
                            {
                                var selectedPediatric =
                                    pediatricPatients.FirstOrDefault(p => p.PatientId == medicationPatientId);

                                if (selectedPediatric != null)
                                {
                                    pediatricMedicationSystem.PrescribeMedication(
                                        selectedPediatric,
                                        medicationName,
                                        patient => patient.Weight > 5);
                                }
                                else
                                {
                                    Console.WriteLine("Pediatric patient not found.");
                                }
                            }
                            else if (medicationType == 2)
                            {
                                var selectedGeriatric =
                                    geriatricPatients.FirstOrDefault(p => p.PatientId == medicationPatientId);

                                if (selectedGeriatric != null)
                                {
                                    geriatricMedicationSystem.PrescribeMedication(
                                        selectedGeriatric,
                                        medicationName,
                                        patient => patient.MobilityScore >= 1);
                                }
                                else
                                {
                                    Console.WriteLine("Geriatric patient not found.");
                                }
                            }
                            break;

                        case "6":
                            exitRequested = true;
                            Console.WriteLine("Exiting system...");
                            break;

                        default:
                            Console.WriteLine("Invalid menu option.");
                            break;
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
        }
    }
}
