[Illias](http://github.com/ahzf/Illias) is an implementation of a distributed RDF QuadStore.    
The implementation ensures an index-free adjacency for fast traversals.    
In the future it will also provide connectors to RDF Stores like [AllegoGraph](http://www.franz.com/agraph/allegrograph) and others and expose itself via the [RDF Storage And Inference Layer (SAIL)](http://www.openrdf.org/doc/sesame/api/org/openrdf/sesame/sail/package-summary.html).

#### Description

 A Quad is a little fragment of a graph with additional information to make the handling of this data structure more easy and scaleable:    
 
    QuadId: Subject -Predicate-> Object [Context/Graph]    
 
 
 Quads can easily be translated to fragments of property graphs:    
 
    VertexId: Vertex -Edge-> AnotherVertex [HyperEdge]    


#### Usage example

    var _QuadStore = new QuadStore<String, String, String, String>(
                             SystemId:        "System1",
                             QuadIdConverter: (QuadId) => QuadId.ToString(),
                             DefaultContext:  ()       => "0");

    var s1 = _QuadStore.Add("Alice", "knows", "Bob");
    var s2 = _QuadStore.Add("Alice", "knows", "Dave");
    var s3 = _QuadStore.Add("Bob",   "knows", "Carol");
    var s4 = _QuadStore.Add("Eve",   "loves", "Alice");
    var s5 = _QuadStore.Add("Carol", "loves", "Alice");


#### Help and Documentation

Additional help and background information can be found in the [Wiki](http://github.com/ahzf/Illias/wiki).
News and updates can also be found on twitter by following: [@ahzf](http://www.twitter.com/ahzf) or [@graphdbs](http://www.twitter.com/graphdbs).

#### Installation

The installation of Illias is very straightforward.    
Just check out or download its sources and all its dependencies:

- [NUnit](http://www.nunit.org/) for unit tests

#### License and your contribution

[Illias](http://github.com/ahzf/Illias) is released under the [GNU Affero General Public License, Version 3](http://www.gnu.org/licenses/agpl.html). For details see the [LICENSE](/ahzf/Illias/blob/master/LICENSE) file.    
To suggest a feature, report a bug or general discussion: [http://github.com/ahzf/Illias/issues](http://github.com/ahzf/Illias/issues)    
If you want to help or contribute source code to this project, please use the same license.   
The coding standards can be found by reading the code ;)

#### Acknowledgments

Please read the [NOTICE](/ahzf/Illias/blob/master/NOTICE) file for further credits.
