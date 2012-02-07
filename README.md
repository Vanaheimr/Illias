[Illias](http://github.com/ahzf/Illias) is an implementation of a distributed QuadStore.

#### Description

 A Quad is a little fragment of a graph with additional information to make the handling of this data structure more easy and scaleable:    
 QuadId: Subject -Predicate-> Object [Context/Graph]    
 
 Quads can easily be translated to fragments of property graphs:    
 VertexId: Vertex -Edge-> AnotherVertex [HyperEdge]    


#### Usage example

    var _TinkerGraph = new PropertyGraph();

    _TinkerGraph.OnVertexAdding += (graph, vertex, vote) => {
        Console.WriteLine("I like all vertices!");
    };

    _TinkerGraph.OnVertexAdding += (graph, vertex, vote) =>
    {
        if (vertex.Id == 5) {
            Console.WriteLine("I'm a Jedi!");
            vote.Veto();
        }
    };

    var marko  = _TinkerGraph.AddVertex(1, v => v.SetProperty("name", "marko"). SetProperty("age",   29));
    var vadas  = _TinkerGraph.AddVertex(2, v => v.SetProperty("name", "vadas"). SetProperty("age",   27));
    var lop    = _TinkerGraph.AddVertex(3, v => v.SetProperty("name", "lop").   SetProperty("lang", "java"));
    var josh   = _TinkerGraph.AddVertex(4, v => v.SetProperty("name", "josh").  SetProperty("age",   32));
    var vader  = _TinkerGraph.AddVertex(5, v => v.SetProperty("name", "darth vader"));
    var ripple = _TinkerGraph.AddVertex(6, v => v.SetProperty("name", "ripple").SetProperty("lang", "java"));
    var peter  = _TinkerGraph.AddVertex(7, v => v.SetProperty("name", "peter"). SetProperty("age",   35));

    Console.WriteLine("Number of vertices added: " + _TinkerGraph.Vertices().Count());

    marko.OnPropertyChanging += (sender, Key, oldValue, newValue, vote) =>
        Console.WriteLine("'" + Key + "' property changing: '" + oldValue + "' -> '" + newValue + "'");

    marko.OnPropertyChanged  += (sender, Key, oldValue, newValue)       =>
        Console.WriteLine("'" + Key + "' property changed: '"  + oldValue + "' -> '" + newValue + "'");


    var _DynamicMarko = marko.AsDynamic();
    _DynamicMarko.age  += 100;
    _DynamicMarko.doIt  = (Action<String>) ((text) => Console.WriteLine("Some infos: " + text + "!"));
    _DynamicMarko.doIt(_DynamicMarko.name + "/" + marko.GetProperty("age") + "/");


    var e7  = _TinkerGraph.AddEdge(marko, vadas,  7,  "knows",   e => e.SetProperty("weight", 0.5));
    var e8  = _TinkerGraph.AddEdge(marko, josh,   8,  "knows",   e => e.SetProperty("weight", 1.0));
    var e9  = _TinkerGraph.AddEdge(marko, lop,    9,  "created", e => e.SetProperty("weight", 0.4));

    var e10 = _TinkerGraph.AddEdge(josh,  ripple, 10, "created", e => e.SetProperty("weight", 1.0));
    var e11 = _TinkerGraph.AddEdge(josh,  lop,    11, "created", e => e.SetProperty("weight", 0.4));

    var e12 = _TinkerGraph.AddEdge(peter, lop,    12, "created", e => e.SetProperty("weight", 0.2));

    return _TinkerGraph;


#### Help and Documentation

Additional help and background information can be found in the [Wiki](http://github.com/ahzf/Illias/wiki).
News and updates can also be found on twitter by following: [@ahzf](http://www.twitter.com/ahzf) or [@graphdbs](http://www.twitter.com/graphdbs).

#### Installation

The installation of Illias is very straightforward.    
Just check out or download its sources and all its dependencies:

- [NUnit](http://www.nunit.org/) for unit tests

#### License and your contribution

[Illias](http://github.com/ahzf/Illias) is released under the [Apache License 2.0](http://www.apache.org/licenses/LICENSE-2.0). For details see the [LICENSE](/ahzf/Illias/blob/master/LICENSE) file.    
To suggest a feature, report a bug or general discussion: [http://github.com/ahzf/Illias/issues](http://github.com/ahzf/Illias/issues)    
If you want to help or contribute source code to this project, please use the same license.   
The coding standards can be found by reading the code ;)

#### Acknowledgments

Please read the [NOTICE](/ahzf/Illias/blob/master/NOTICE) file for further credits.
