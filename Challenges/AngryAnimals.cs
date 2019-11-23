using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Solution.Main;

namespace Solution
{
    public class Main {


        public class ListAnimals
        {
            public ListAnimals()
            {

            }
            public ListAnimals(int animal)
            {
                this.NumberAnimal = animal;
                this.PossibleGroups = new List<List<int>>();
                this.PossibleGroups.Add(new List<int> { animal });
            }
            public int NumberAnimal { get; set; }
            public List<List<int>> PossibleGroups { get; set; }
            public void Clear()
            {
                this.PossibleGroups.Clear();
                this.NumberAnimal = 0;
            }
        }
    }

    public class AngryAnimals5
    {
        public static long AngryAnimals(int n, List<int> a, List<int> b)
        {
            List<List<int>> forbidden = GetAllForbidden(a, b);
            List<Main.ListAnimals> groups = new List<Main.ListAnimals>();
            long total = 0;
            bool cont = true;
            List<int> group = new List<int>();
            var la = new Main.ListAnimals();
            for (var animal = 1; animal <= n; animal++)
            {
                la = new Main.ListAnimals(animal);
                cont = true;
                group.Clear();
                var refAnimal = 1;

                if (groups.Count > 0)
                {
                    group = GetTheLastProcessed(groups, animal);
                    if (group.Count == 0)
                    {
                        group.Add(animal);
                    }
                    else
                    {
                        refAnimal = group.Last() - animal;
                        refAnimal = refAnimal == 0 ? 1 : refAnimal + 1;
                        la.PossibleGroups.Add(group);
                    }
                }
                else
                {
                    group.Add(animal);
                }
                do
                {
                    if (animal + refAnimal > n)
                    {
                        cont = false;
                    }
                    else
                    {
                        cont = CanIAddThisOne(animal + refAnimal, group, forbidden);
                        if (cont)
                        {
                            group.Add(animal + refAnimal);
                            la.PossibleGroups.Add(group.ToList());
                            refAnimal++;
                        }
                    }
                } while (cont);
                var l = la.PossibleGroups.Last();
                la.PossibleGroups.Clear();
                la.PossibleGroups.Add(l);
                total += l.Count;
                groups.Add(la);
            }
            return total;
        }

        private static bool CanIAddThisOne(int v, List<int> group, List<List<int>> forbidden)
        {
            var f = forbidden.Where(x => x[0] == v || x[1] == v).Select(y => y[0] == v ? y[1] : y[0]).ToList().Any(x => group.Any(k => k == x));
            return !f;
        }

        private static List<int> GetTheLastProcessed(List<Main.ListAnimals> groups, int animal)
        {
            var _g = groups.Last().PossibleGroups[0].ToList();
            if (_g.Count > 0)
                _g.RemoveAt(0);
            return _g;
        }

        private static List<List<int>> GetAllForbidden(List<int> a, List<int> b, bool r = false)
        {
            List<List<int>> _forbidden = new List<List<int>>();
            for (var f = 0; f < a.Count; f++)
            {
                List<int> _arr = new List<int> { a[f], b[f] };
                _arr.Sort();
                if (r)
                    _arr.Reverse();
                _forbidden.Add(_arr);
            }
            _forbidden.Sort((x, y) => x[0].CompareTo(y[0]));
            return _forbidden;
        }
    }

    public class AngryAnimals4
    {
        public static long angryAnimals(int n, List<int> a, List<int> b)
        {
            List<List<int>> fbda = GetAllForbidden(a, b);
            List<List<int>> fbdb = GetAllForbidden(a, b, true);
            List<Main.ListAnimals> groups = new List<Main.ListAnimals>();
            long total = 0;
            bool cont = true;
            List<int> group = new List<int>();
            var la = new Main.ListAnimals();
            for (var animal = 1; animal <= n; animal++)
            {
                la = new Main.ListAnimals(animal);
                cont = true;
                group.Clear();
                var refAnimal = 1;

                if (groups.Count > 0)
                {
                    group = GetTheLastProcessed(groups, animal);
                    if (group.Count == 0)
                    {
                        group.Add(animal);
                    }
                    else
                    {
                        refAnimal = group.Last() - animal;
                        refAnimal = refAnimal == 0 ? 1 : refAnimal + 1;
                        la.PossibleGroups.Add(group);
                    }
                }
                else
                {
                    group.Add(animal);
                }
                do
                {
                    if (animal + refAnimal > n)
                    {
                        cont = false;
                    }
                    else
                    {
                        cont = CanIAddThisOne(animal + refAnimal, group, fbda, fbdb);
                        if (cont)
                        {
                            group.Add(animal + refAnimal);
                            //group = group.Distinct().ToList();
                            la.PossibleGroups.Add(group.ToList());
                            refAnimal++;
                        }
                    }
                } while (cont);
                var l = la.PossibleGroups.Last();
                la.PossibleGroups.Clear();
                la.PossibleGroups.Add(l);
                total += l.Count;
                groups.Add(la);
            }
            return total;
        }

        private static bool CanIAddThisOne(int v, List<int> group, List<List<int>> fbda, List<List<int>> fbdb)
        {
            var f = new List<int>();
            for (var _i = 0; _i < fbda.Count && fbda[_i][0] <= v; _i++)
            {
                if (fbda[_i][0] == v)
                {
                    f.Add(fbda[_i][1]);
                }
            }

            for (var _i = 0; _i < fbdb.Count && fbdb[_i][0] <= v; _i++)
            {
                if (fbdb[_i][0] == v)
                {
                    f.Add(fbdb[_i][1]);
                }
            }

            var r = true;
            f = f.Distinct().ToList();
            for (var _i = 0; _i < f.Count && r; _i++)
            {
                for (int _j = 0; _j < group.Count && r; _j++)
                {
                    if (group[_j] == f[_i])
                    {
                        r = false;
                        break;
                    }
                }
            }
            return r;
        }

        private static List<int> GetTheLastProcessed(List<Main.ListAnimals> groups, int animal)
        {
            var _g = groups.Last().PossibleGroups[0].ToList();
            if (_g.Count > 0)
                _g.RemoveAt(0);
            return _g;
        }

        private static List<List<int>> GetAllForbidden(List<int> a, List<int> b, bool r = false)
        {
            List<List<int>> _forbidden = new List<List<int>>();
            for (var f = 0; f < a.Count; f++)
            {
                List<int> _arr = new List<int> { a[f], b[f] };
                _arr.Sort();
                if (r)
                    _arr.Reverse();
                _forbidden.Add(_arr);
            }
            _forbidden.Sort((x, y) => x[0].CompareTo(y[0]));
            return _forbidden;
        }
    }

    public class AngryAnimals3
    {
        public static long AngryAnimals(int n, List<int> a, List<int> b)
        {
            List<List<int>> forbidden = GetAllforbidden(a, b);
            List<Main.ListAnimals> groups = new List<Main.ListAnimals>();
            long total = 0;
            bool cont = true;
            List<int> group = new List<int>();
            var la = new Main.ListAnimals();
            for (var animal = 1; animal <= n; animal++)
            {
                la = new Main.ListAnimals(animal);
                cont = true;
                group.Clear();
                var refAnimal = 1;

                if (groups.Count > 0)
                {
                    group = GetTheLastProcessed(groups, animal);
                    if (group.Count == 0)
                    {
                        group.Add(animal);
                    }
                    else
                    {
                        refAnimal = group.Last() - animal;
                        refAnimal = refAnimal == 0 ? 1 : refAnimal;
                    }
                }
                else
                {
                    group.Add(animal);
                }

                do
                {
                    if (animal + refAnimal > n)
                    {
                        cont = false;
                    }
                    else
                    {
                        cont = CanIAddThisOne(animal + refAnimal, group, forbidden);
                        if (cont)
                        {
                            group.Add(animal + refAnimal);
                            group = group.Distinct().ToList();
                            la.PossibleGroups.Add(group.ToList());
                            refAnimal++;
                        }
                    }
                } while (cont);
                var l = la.PossibleGroups.Last();
                la.PossibleGroups.Clear();
                la.PossibleGroups.Add(l);
                total += l.Count;
                groups.Add(la);
            }
            return total;
        }

        private static List<int> GetTheLastProcessed(List<Main.ListAnimals> groups, int animal)
        {
            var _g = groups.Last().PossibleGroups[0].ToList();
            if (_g.Count > 0)
                _g.RemoveAt(0);
            return _g;
        }

        private static bool CanIAddThisOne(int v, List<int> group, List<List<int>> forbidden)
        {
            var _allforb = forbidden.Where(x => x[0] == v || x[1] == v).Select(y => y[0] == v ? y[1] : y[0]).ToList();
            _allforb.Sort();
            _allforb.Distinct();
            var r = true;
            _allforb.ForEach(x =>
            {
                group.ForEach(y =>
                {
                    if (y == x)
                    {
                        r = false;
                    }
                });
            });

            return r;
        }

        private static List<List<int>> GetAllforbidden(List<int> a, List<int> b)
        {
            List<List<int>> _forbidden = new List<List<int>>();
            for (var f = 0; f < a.Count; f++)
            {
                _forbidden.Add(new List<int> { a[f], b[f] });
            }
            return _forbidden;
        }
    }

    public class AngryAnimals2
    {
        public static long AngryAnimals(int n, List<int> a, List<int> b)
        {
            List<List<int>> forbidden = GetAllForbidden(a, b);

            int trueSequence = 0;
            List<int> ns = new List<int>();
            int av = 1;
            bool isThere = true;
            for (var animal = 1; animal < n; animal++)
            {
                isThere = true;
                ns.Add(animal);
                av = 1;
                do
                {
                    var preCalculedForbidden = GetAllForbiddenValuesForThisAnimal(animal + av, forbidden);
                    isThere = IsThereAForbiddenNumber(ns, preCalculedForbidden);
                    if (!isThere)
                    {
                        ns.Add(animal + av);
                        av++;
                        trueSequence++;
                    }
                } while (!isThere && ((av + animal) <= n));
                ns.Clear();
            }
            return trueSequence + n;
        }

        private static bool IsThereAForbiddenNumber(List<int> ns, List<int> preCalculedForbidden)
        {
            var r = false;
            preCalculedForbidden.ForEach(x =>
            {
                ns.ForEach(y =>
                {
                    if (y == x)
                    {
                        r = true;
                    }
                });
            });
            return r;
        }

        private static List<int> GetAllForbiddenValuesForThisAnimal(int v, List<List<int>> forbidden)
        {
            return forbidden.Where(x => x[0] == v || x[1] == v).Select(y => y[0] == v ? y[1] : y[0]).ToList();
        }

        private static List<List<int>> GetAllForbidden(List<int> a, List<int> b)
        {
            List<List<int>> _forbidden = new List<List<int>>();
            for (var f = 0; f < a.Count; f++)
            {
                List<int> _arr = new List<int> { a[f], b[f] };
                _arr.Sort();
                _forbidden.Add(_arr);
            }
            _forbidden.Sort((x, y) => x[0].CompareTo(y[0]));
            return _forbidden;
        }
    }

    //fully optimized
    public class AngryAnimals1
    {
        public static long AngryAnimals(int n, List<int> a, List<int> b)
        {
            List<List<int>> fbda = GetAllForbidden(a, b);
            List<List<int>> fbdb = GetAllForbidden(a, b, true);

            int trueSequence = 0;
            List<int> ns = new List<int>();
            int av = 1;
            bool cont = true;
            for (var animal = 1; animal < n; animal++)
            {
                cont = true;
                ns.Add(animal);
                av = 1;
                List<int> preCalculedForbidden = new List<int>();
                fbda = fbda.Where(x => x[0] >= animal && x[1] >= animal).ToList();
                fbdb = fbdb.Where(x => x[0] >= animal && x[1] >= animal).ToList();
                do
                {
                    preCalculedForbidden = GetAllForbiddenValuesForThisArray(animal + av, preCalculedForbidden, fbda, fbdb);
                    cont = IsThereAForbiddenNumber(ns, preCalculedForbidden);
                    if (cont)
                    {
                        ns.Add(animal + av);
                        av++;
                        trueSequence++;
                    }
                } while (cont && ((av + animal) <= n));
                preCalculedForbidden.Clear();
                ns.Clear();
            }
            return (long)(trueSequence + n);
        }
        private static bool IsThereAForbiddenNumber(List<int> ns, List<int> preCalculedForbidden)
        {
            var r = false;
            var lt = preCalculedForbidden;
            lt.Reverse();
            for (var _i = ns.Count - 1; _i >= 0 && !r; _i--)
            {
                for (var _j = lt.Count - 1; _j >= 0 && !r && ns[_i] >= lt[_j]; _j--)
                {
                    if (ns[_i] == lt[_j])
                    {
                        r = true;
                    }
                }
            }

            return !r;
        }
        private static List<int> GetAllForbiddenValuesForThisArray(int v, List<int> preCalculedForbidden, List<List<int>> fbda, List<List<int>> fbdb)
        {
            List<int> _cannotHave = new List<int>();

            for (var _i = 0; _i < fbda.Count && fbda[_i][0] <= v; _i++)
            {
                if (fbda[_i][0] == v)
                    _cannotHave.Add(fbda[_i][1]);
            }

            for (var _i = 0; _i < fbdb.Count && fbdb[_i][0] <= v; _i++)
            {
                if (fbdb[_i][0] == v)
                    _cannotHave.Add(fbdb[_i][1]);
            }
            preCalculedForbidden = preCalculedForbidden.Concat(_cannotHave).ToList();
            preCalculedForbidden = preCalculedForbidden.Distinct().ToList();
            preCalculedForbidden.Sort();
            return preCalculedForbidden;
        }
        private static List<List<int>> GetAllForbidden(List<int> a, List<int> b, bool r = false)
        {
            List<List<int>> _forbidden = new List<List<int>>();
            for (var f = 0; f < a.Count; f++)
            {
                List<int> _arr = new List<int> { a[f], b[f] };
                _arr.Sort();
                if (r)
                    _arr.Reverse();
                _forbidden.Add(_arr);
            }
            _forbidden.Sort((x, y) => x[0].CompareTo(y[0]));
            return _forbidden;
        }
    }
}
