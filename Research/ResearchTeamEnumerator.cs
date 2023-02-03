using System.Collections;

namespace Research;

public partial class ResearchTeam
{
    public class ResearchTeamEnumerator : IEnumerator<Person>
    {
        private readonly ResearchTeam _researchTeam;

        private int _index = -1;

        public ResearchTeamEnumerator(ResearchTeam researchTeam) => _researchTeam = researchTeam;

        public Person Current => _researchTeam.Members[_index];

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            if (_index < _researchTeam.Members.Count - 1)
            {
                _index++;
                return true;
            }
            return false;
        }

        public void Reset() => _index = -1;
    }
}