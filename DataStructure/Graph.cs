using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoGrafos.DataStructure
{
    /// <summary>
    /// Classe que representa um grafo.
    /// </summary>
    public class Graph
    {
        private List<Node> nodes;

        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }
        
        public Graph()
        {
            this.nodes = new List<Node>();
        }
        public List<Node> ShortestPath(string begin, string end)
        {
            List<Node> path = new List<Node>();
            Graph graph = new Graph();
            Node node = new Node(begin, 0);

            node.Parent = null;
            graph.nodes.Add(node);
            path.Add(node);

            while (node.Name.Equals(end) == false)
            {
                node = null;
                foreach (Node n in graph.nodes)
                {
                    Node nodeOrigin = this.FNode(n.Name);
                    foreach (Edge edOrigin in nodeOrigin.Edges)
                    {

                        Node auxno = graph.FNode(edOrigin.To.Name);
                        if (auxno == null)
                        {
                            if (node == null || (Convert.ToDouble(n.Info) + edOrigin.Cost) < Convert.ToDouble(n.Info))
                            {
                                node.Parent = n;
                                path.Add(edOrigin.To);
                                node = new Node(edOrigin.To.Name, Convert.ToDouble(n.Info) + edOrigin.Cost);
                                edOrigin.To.Parent = edOrigin.From;
                            }
                        }
                    }
                }
                graph.nodes.Add(node);
                graph.AddEdge(node.Name, node.Parent.Name, 0);
            }
            return path;
        }

        public List<Node> BreadthFirstSearch(string begin)
        {
            List<Node> list = new List<Node>();
            Queue<Node> queu = new Queue<Node>();

            Node n = this.FNode(begin);
            n.Visited = true;
            queu.Enqueue(n);
            list.Add(n);
            while (queu.Count > 0)
            {
                Node current = queu.Dequeue();
                foreach (Edge e in current.Edges)
                {
                    if (!e.To.Visited)
                    {
                        queu.Enqueue(e.To);
                        e.To.Visited = true;
                        e.To.Parent = current;
                        list.Add(e.To);
                    }
                }
            }
            return list;
        }

        public List<Node> DepthFirstSearch(string begin)
        {
            List<Node> list = new List<Node>();
            Stack<Node> queu = new Stack<Node>();

            Node n = this.FNode(begin);
            n.Visited = true;
            queu.Push(n);
            list.Add(n);

            while (queu.Count > 0)
            {
                bool Numb = false;
                Node current = queu.Peek();
                foreach (Edge e in current.Edges)
                {
                    if (!e.To.Visited)
                    {
                        queu.Push(e.To);
                        e.To.Parent = current;
                        e.To.Visited = true;
                        list.Add(e.To);
                        Numb = true;
                        break;
                    }
                }
                if (!Numb)
                    queu.Pop();
            }

            return list;
        }
        public Node FNode(string name)
        {
            foreach (Node n in nodes)
            {
                if (n.Name.Equals(name))
                {
                    return n;
                }
            }
            return null;
        }
        public void AddNode(string name, object info)
        {
            if (this.FNode(name) == null)
            {
                nodes.Add(new Node(name, info));
            }
        }

        public void AddEdge(string nameFrom, string nameTo, int cost)
        {
            Node fr = this.FNode(nameFrom);
            Node to = this.FNode(nameTo);
            if (fr != null && to != null)
            {
                fr.AddEdge(to, cost);
            }

        }

    }
}
