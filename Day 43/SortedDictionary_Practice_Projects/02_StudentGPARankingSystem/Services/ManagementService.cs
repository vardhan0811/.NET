using System.Collections.Generic;
using System.Linq;
using Domain;
using Exceptions;

namespace Services
{
    public class ManagementService
    {
        // Key = GPA
        // Value = Students having same GPA
        private SortedDictionary<double, List<BaseEntity>> _data
            = new SortedDictionary<double, List<BaseEntity>>();

        // Add Student
        public void AddEntity(double key, BaseEntity entity)
        {
            entity.Validate();

            // Duplicate check
            foreach (var list in _data.Values)
            {
                if (list.Exists(e => e.Id == entity.Id))
                    throw new ScenarioException("Duplicate Student");
            }

            if (!_data.ContainsKey(key))
                _data[key] = new List<BaseEntity>();

            _data[key].Add(entity);
        }

        // Update GPA
        public void UpdateEntity(string id, double newGpa)
        {
            if (newGpa < 0 || newGpa > 10)
                throw new ScenarioException("Invalid GPA");

            foreach (var pair in _data)
            {
                var student = pair.Value.Find(e => e.Id == id);
                if (student != null)
                {
                    pair.Value.Remove(student);

                    if (!_data.ContainsKey(newGpa))
                        _data[newGpa] = new List<BaseEntity>();

                    _data[newGpa].Add(student);
                    return;
                }
            }

            throw new ScenarioException("Student Not Found");
        }

        // Remove Student
        public void RemoveEntity(string id)
        {
            foreach (var pair in _data)
            {
                var student = pair.Value.Find(e => e.Id == id);
                if (student != null)
                {
                    pair.Value.Remove(student);
                    return;
                }
            }

            throw new ScenarioException("Student Not Found");
        }

        // Display Ranking (Descending GPA)
        public IEnumerable<BaseEntity> GetAll()
        {
            foreach (var pair in _data.Reverse())
            {
                foreach (var entity in pair.Value)
                    yield return entity;
            }
        }
    }
}
