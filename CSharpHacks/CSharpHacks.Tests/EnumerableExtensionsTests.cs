﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace CSharpHacks.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void should_return_distinct_list_by_key()
        {
            var source = new List<(int Key, string Value)>
            {
                (1, "Test"),
                (2, "Test"),
                (2, "Test"),
                (3, "Test")
            };

            var result = source.DistinctBy(x => x.Key).ToList();
            result.Count.Should().Be(3);
            result.Count(x => x.Key == 2).Should().Be(1);
        }

        [Fact]
        public void should_return_empty_enumerable_if_null()
        {
            List<int> nullList = null;

            var result = nullList.EmptyIfNull();
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void should_return_input_enumerable_if_not_null()
        {
            List<int> list = new List<int>{1, 2, 3};

            var result = list.EmptyIfNull();
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
        }

        [Fact]
        public void should_return_minimum_by_selector()
        {
            var source = new List<(int Key, string Value)>
            {
                (1, "ZZZ"),
                (1, "ABC"),
                (2, "YYY"),
                (3, "AAA")
            };

            var result = source.SelectByMin(x => x.Key).ToList();
            result.Count.Should().Be(2);
            result.Should().Contain(x => x.Value == "ZZZ");
            result.Should().Contain(x => x.Value == "ABC");
        }

        [Fact]
        public void should_return_maximum_by_selector()
        {
            var source = new List<(int Key, string Value)>
            {
                (3, "ZZZ"),
                (3, "ABC"),
                (2, "YYY"),
                (1, "AAA")
            };

            var result = source.SelectByMax(x => x.Key).ToList();
            result.Count.Should().Be(2);
            result.Should().Contain(x => x.Value == "ZZZ");
            result.Should().Contain(x => x.Value == "ABC");
        }

        [Fact]
        public void should_take_first_two_elements_by_key()
        {
            var source = new List<(int Key, string Value)>
            {
                (3, "AAA"),
                (3, "BBB"),
                (3, "CCC"),
                (2, "DDD"),
                (2, "EEE"),
                (1, "FFF")
            };

            var result = source.TakePerKey(x => x.Key, 2).ToList();
            result.Count.Should().Be(5);
            result.Should().NotContain(x => x.Value == "CCC");
        }
        
        [Fact]
        public void Omit_should_omit_last_x_elements_of_an_enumerable()
        {
            int[] expected = {1, 2, 3, 4, 5, 6};
            int[] test = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            Assert.Equal(expected, test.Omit(4));
        }

        [Fact]
        public void Omit_should_return_empty_enumerable_if_number_is_larger_than_size_of_enumerable()
        {
            int[] expected = { };
            int[] test = {1, 2, 3, 4, 5, 6};
            Assert.Equal(expected, test.Omit(1000));
        }

        [Fact]
        public void Last_should_Return_last_x_elements()
        {
            int[] expected = {7, 8, 9};
            int[] test = {1, 2, 3, 4, 5, 6, 7, 8, 9};
            Assert.Equal(expected, test.Last(3));
        }

        [Fact]
        public void Last_should_return_whole_array_if_number_exceeds_element_size()
        {
            int[] expected = {1, 2, 3, 4, 5, 6, 7, 8, 9};
            int[] test = {1, 2, 3, 4, 5, 6, 7, 8, 9};
            Assert.Equal(expected, test.Last(1000));
        }

        [Fact]
        public void Occurence_should_return_frequency_of_each_element()
        {
            string[] test = {"a", "ab", "a", "c", "b", "ab", "a", "hi", "my name", "a", "c"};
            var expected = new Dictionary<string, int>()
            {
                {"a", 4},
                {"ab", 2},
                {"b",1},
                {"c", 2},
                {"hi", 1},
                {"my name", 1}
            };
            Assert.Equal(expected, test.Occurrence());
        }

        [Fact]
        public void Batch_should_return_groups_of_given_elements()
        {
            int[] test = { 1, 2, 3, 4, 5, 6, 7};
            var expected = new[] 
            {
                new[] {1,2},
                new[] {3,4},
                new[] {5,6},
                new[] {7}
            };

            var result = test.Batch(2).ToArray();

            result.Length.Should().Be(expected.Length);
            for (int index = 0; index < expected.Length; index++)
            {
                result[index].Should().Equal(expected[index]);
            }
        }
    }
}
