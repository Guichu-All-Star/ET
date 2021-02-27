using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ETModel
{

    [ObjectSystem]
    public class UnitComponentUpdateSystem : UpdateSystem<UnitComponent>
    {
        public override void Update(UnitComponent self)
        {
            self.Update();
        }
    }
    public class UnitComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private readonly Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (Unit unit in this.idUnits.Values)
            {
                unit.Dispose();
            }
            idUnits.Clear();
        }

        public void Add(Unit unit)
        {
            idUnits.Add(unit.Id, unit);
        }

        public Unit Get(long id)
        {
            idUnits.TryGetValue(id, out Unit unit);
            return unit;
        }

        public void Remove(long id)
        {
            Unit unit;
            if (idUnits.TryGetValue(id, out unit))
            {
                idUnits.Remove(id);
                unit.Dispose();
            }
        }

        public void RemoveNoDispose(long id)
        {
            idUnits.Remove(id);
        }

        public int Count
        {
            get
            {
                return idUnits.Count;
            }
        }

        public Unit[] GetAll()
        {
            return idUnits.Values.ToArray();
        }

        public void Update() 
        {

        }
    }
}