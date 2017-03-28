﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Test
{
    public class Tests
    {
        [Theory]
        [MemberData("DataBinary")]
        public void Test1(int[] values, int from, int to, int expected)
        {
            BinarySearchTreeFinder treeFinder = new BinarySearchTreeFinder();
            int distance = treeFinder.FindDistance(values, from, to);
            distance.Should().Be(expected);
        }

        public static IList<object[]> DataBinary()
        {
            return new List<object[]>
            {
                new object[] {new object[] {5, 6, 3, 1, 2, 4}, 6, 2, 4}
            };
        }
    }

    public class BinarySearchTreeFinder
    {
        public int FindDistance(int[] values, int from, int to)
        {
            BinarySearchTree tree = new BinarySearchTree();
            foreach (var value in values)
            {
                tree.Add(value);
            }
            return tree.Distance(from, to);
        }
    }

    public class BinarySearchTree
    {
        private Node root;

        public void Add(int value)
        {
            if (this.root == null)
            {
                this.root = new Node(value);
            }
            else
            {
                this.root.Add(value);
            }
        }

        public int Distance(int from, int to)
        {
//            int levelFrom = this.root.Search(@from);
//            int levelTo = this.root.Search(to);
//
//            if (levelFrom == -1 || levelTo == -1)
//            {
//                return -1;
//            }
            Node node = this.root.FindCommonNode(from, to);
            if (node == null)
            {
                return -1;
            }
            int distanceFrom = node.Distance(0,from);
            int distanceTo = node.Distance(0,to);

            return distanceFrom + distanceTo;
        }
    }

    internal class Node
    {
        private Node left;
        private Node right;
        private readonly int value;

        public Node(int input)
        {
            this.value = input;
        }

        public void Add(int input)
        {
            if (input < this.value)
            {
                if (this.left == null)
                {
                    this.left = new Node(input);
                }
                else
                {
                    this.left.Add(input);
                }
            }
            else
            {
                if (this.right == null)
                {
                    this.right = new Node(input);
                }
                else
                {
                    this.right.Add(input);
                }
            }
        }

        public Node FindCommonNode(int from, int to)
        {
            if (this.value < from && this.value<to)
            {
                return this.left.FindCommonNode(from, to);
            }
            if(this.value>= from&&this.value>=to)
            {
                return this.right.FindCommonNode(from, to);
            }
            return this;
        }

        public int Distance(int count ,int from)
        {

            if (this.value==from)
            {
                return count;
            }
            if (this.value<from)
            {
                return this.left.Distance(count + 1, from);
            }
            if (this.value>from)
            {
                return this.right.Distance(count + 1, from);
            }
            return 0;
        }
    }
}