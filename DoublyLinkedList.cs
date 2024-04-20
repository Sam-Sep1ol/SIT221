using System;
using System.Text;

namespace _5._1P_Basic_List_Operations
{
    public class DoublyLinkedList<T>
    {

        // Nested Node<K> class represents individual nodes in the DoublyLinkedList<T>
        private class Node<K> : INode<K>
        {
            public K Value { get; set; }
            public Node<K> Next { get; set; }
            public Node<K> Previous { get; set; }

            // Node constructor initializes a node with a given value, previous node, and next node
            public Node(K value, Node<K> previous, Node<K> next)
            {
                Value = value;
                Previous = previous;
                Next = next;
            }

            // ToString() method represents a node as a tuple {'previous node's value'-(node's value)-'next node's value')}. 
            // 'XXX' is used when the current node matches the First or the Last of the DoublyLinkedList<T>
            public override string ToString()
            {
                StringBuilder s = new StringBuilder();
                s.Append("{");
                s.Append(Previous.Previous == null ? "XXX" : Previous.Value.ToString());
                s.Append("-(");
                s.Append(Value);
                s.Append(")-");
                s.Append(Next.Next == null ? "XXX" : Next.Value.ToString());
                s.Append("}");
                return s.ToString();
            }

        }

        // DoublyLinkedList<T> class begins with the description of its methods and attributes

        // The use of two auxiliary nodes: Head and Tail, significantly simplifies the implementation of the class and makes insertion functionality reduced just to AddBetween(...)
        // These properties are private, thus invisible to a user of the data structure, but are always maintained in it, even when the DoublyLinkedList<T> is formally empty. 
        private Node<T> Head { get; set; }
        private Node<T> Tail { get; set; }
        public int Count { get; private set; } = 0;

        // Constructor initializes the Head and Tail nodes, and links them together
        public DoublyLinkedList()
        {
            Head = new Node<T>(default(T), null, null);
            Tail = new Node<T>(default(T), Head, null);
            Head.Next = Tail;
        }

        // First property returns the first node in the list, or null if the list is empty
        public INode<T> First
        {
            get
            {
                if (Count == 0) return null;
                else return Head.Next;
            }
        }

        // Last property returns the last node in the list, or null if the list is empty
        public INode<T> Last
        {
            get
            {
                if (Count == 0) return null;
                else return Tail.Previous;
            }
        }

        // After method returns the node after the specified node
        public INode<T> After(INode<T> node)
        {
            if (node == null) throw new NullReferenceException();
            Node<T> node_current = node as Node<T>;
            if (node_current.Previous == null || node_current.Next == null) throw new InvalidOperationException("The node referred as 'before' is no longer in the list");
            if (node_current.Next.Equals(Tail)) return null;
            else return node_current.Next;
        }

        // AddLast method adds a new node with the specified value at the end of the list
        public INode<T> AddLast(T value)
        {
            return AddBetween(value, Tail.Previous, Tail);
        }

        // AddBetween method creates a new node and inserts it between the given previous and next nodes
        private Node<T> AddBetween(T value, Node<T> previous, Node<T> next)
        {
            Node<T> node = new Node<T>(value, previous, next);
            previous.Next = node;
            next.Previous = node;
            Count++;
            return node;
        }

        // Find method searches for a node with the specified value and returns it, or null if not found
        public INode<T> Find(T value)
        {
            Node<T> node = Head.Next;
            while (!node.Equals(Tail))
            {
                if (node.Value.Equals(value)) return node;
                node = node.Next;
            }
            return null;
        }

        // ToString method creates a string representation of the DoublyLinkedList<T>
        public override string ToString()
        {
            if (Count == 0) return "[]";
            StringBuilder s = new StringBuilder();
            s.Append("[");
            int k = 0;
            Node<T> node = Head.Next;
            while (!node.Equals(Tail))
            {
                s.Append(node.ToString());
                node = node.Next;
                if (k < Count - 1) s.Append(",");
                k++;
            }
            s.Append("]");
            return s.ToString();
        }

        // TODO: Your task is to implement all the remaining methods.
        // Read the instruction carefully, study the code examples from above as they should help you to write the rest of the code.

        // Before method returns the node before the specified node
        public INode<T> Before(INode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Node<T> currentNode = Head.Next;

            while (!currentNode.Equals(Tail))
            {
                if (currentNode.Equals(node))
                {
                    // If the specified node is found, return the previous node.
                    if (currentNode.Previous.Equals(Head))
                    {
                        // If the previous node is the Head, the specified node is the first node.
                        return null;
                    }
                    else
                    {
                        return currentNode.Previous;
                    }
                }

                currentNode = currentNode.Next;
            }

            // If the specified node is not found in the list, throw an exception.
            throw new InvalidOperationException("The specified node is not in the current DoublyLinkedList<T>.");
        }

        // AddFirst method adds a new node with the specified value at the beginning of the list
        public INode<T> AddFirst(T value)
        {
            // Create a new node and add it to the beginning of the list.
            Node<T> newNode = AddBetween(value, Head, Head.Next);
            return newNode;
        }

        // AddBefore method adds a new node with the specified value before the specified node
        public INode<T> AddBefore(INode<T> before, T value)
        {
            if (before == null)
            {
                throw new ArgumentNullException(nameof(before));
            }

            Node<T> beforeNode = before as Node<T>;

            // Check if the specified node is in the current DoublyLinkedList<T>.
            if (beforeNode == null || beforeNode.Previous == null || beforeNode.Next == null)
            {
                throw new InvalidOperationException("The specified node is not in the current DoublyLinkedList<T>.");
            }

            // Add a new node before the specified node.
            Node<T> newNode = AddBetween(value, beforeNode.Previous, beforeNode);
            return newNode;
        }

        // AddAfter method adds a new node with the specified value after the specified node
        public INode<T> AddAfter(INode<T> after, T value)
        {
            if (after == null)
            {
                throw new ArgumentNullException(nameof(after));
            }

            Node<T> afterNode = after as Node<T>;

            // Check if the specified node is in the current DoublyLinkedList<T>.
            if (afterNode == null || afterNode.Previous == null || afterNode.Next == null)
            {
                throw new InvalidOperationException("The specified node is not in the current DoublyLinkedList<T>.");
            }

            // Add a new node after the specified node.
            Node<T> newNode = AddBetween(value, afterNode, afterNode.Next);
            return newNode;
        }

        // Clear method removes all nodes from the DoublyLinkedList<T> and nullifies links
        public void Clear()
        {
            // Remove all nodes from the DoublyLinkedList<T> and nullify links.
            while (Count > 0)
            {
                Remove(Head.Next);
            }
        }

        // Remove method removes the specified node from the DoublyLinkedList<T>
        public void Remove(INode<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Node<T> nodeToRemove = node as Node<T>;

            // Check if the specified node is in the current DoublyLinkedList<T>.
            if (nodeToRemove == null || nodeToRemove.Previous == null || nodeToRemove.Next == null)
            {
                throw new InvalidOperationException("The specified node is not in the current DoublyLinkedList<T>.");
            }

            // Remove the specified node from the DoublyLinkedList<T>.
            nodeToRemove.Previous.Next = nodeToRemove.Next;
            nodeToRemove.Next.Previous = nodeToRemove.Previous;
            nodeToRemove.Previous = null;
            nodeToRemove.Next = null;
            Count--;
        }

        // RemoveFirst method removes the node at the start of the DoublyLinkedList<T>
        public void RemoveFirst()
        {
            // Check if the DoublyLinkedList<T> is empty.
            if (Count == 0)
            {
                throw new InvalidOperationException("The DoublyLinkedList<T> is empty.");
            }

            // Remove the node at the start of the DoublyLinkedList<T>.
            Remove(Head.Next);
        }

        // RemoveLast method removes the node at the end of the DoublyLinkedList<T>
        public void RemoveLast()
        {
            // Check if the DoublyLinkedList<T> is empty.
            if (Count == 0)
            {
                throw new InvalidOperationException("The DoublyLinkedList<T> is empty.");
            }

            // Remove the node at the end of the DoublyLinkedList<T>.
            Remove(Tail.Previous);
        }

    }
}
