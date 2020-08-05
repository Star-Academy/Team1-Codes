package search.test;

import org.junit.*;
import search.*;
import java.util.*;

public class QueryTest{
    Query query;
    @Before
    public void setUp(){
        query = new Query(new InvertedIndex());

        //////////////////////////////

        HashSet<String> docs1 = new HashSet<>();
        docs1.add("d1");
        query.invertedIndex.allWords.put("or1", docs1);

        HashSet<String> docs2 = new HashSet<>();
        docs2.add("d2");
        query.invertedIndex.allWords.put("or2", docs2);

        HashSet<String> docs3 = new HashSet<>();
        docs3.add("d3");
        query.invertedIndex.allWords.put("or3", docs3);

        HashSet<String> docs4 = new HashSet<>();
        docs4.add("d2");
        docs4.add("d1");
        query.invertedIndex.allWords.put("and1", docs4);

        HashSet<String> docs5 = new HashSet<>();
        docs5.add("d1");
        docs5.add("d2");
        query.invertedIndex.allWords.put("and2", docs5);

        HashSet<String> docs8 = new HashSet<>();
        docs8.add("d4");
        query.invertedIndex.allWords.put("and3", docs8);

        HashSet<String> docs6 = new HashSet<>();
        docs6.add("d1");
        query.invertedIndex.allWords.put("exc1", docs6);

        HashSet<String> docs7 = new HashSet<>();
        docs7.add("d3");
        query.invertedIndex.allWords.put("exc2", docs7);

        /////////////////////////////////
    }

    @Test
    public void analyzeQueryTest(){
        String[] input = {"+or1" , "+or2", "and1", "+or3", "-exc1", "-exc2", "and2"};
        query.analyzeQuery(input);
        ArrayList<String> expectedOrQueries = new ArrayList<>();
        expectedOrQueries.add("or1");
        expectedOrQueries.add("or2");
        expectedOrQueries.add("or3");
        ArrayList<String> expectedAndQueries = new ArrayList<>();
        expectedAndQueries.add("and1");
        expectedAndQueries.add("and2");
        ArrayList<String> expectedExcQueries = new ArrayList<>();
        expectedExcQueries.add("exc1");
        expectedExcQueries.add("exc2");

        Assert.assertEquals(query.orQueries, expectedOrQueries);
        Assert.assertEquals(query.andQueries, expectedAndQueries);
        Assert.assertEquals(query.excQueries, expectedExcQueries);
    }

    @Test
    public void buildResultTest1(){
        query.orQueries.add("or1");      // d1
        query.orQueries.add("or2");      // d2
        query.orQueries.add("or3");      // d3

        query.andQueries.add("and1");       // d1,d2
        query.andQueries.add("and2");       // d1,d2

        query.excQueries.add("exc1");       // d1
        query.excQueries.add("exc2");       // d3

        query.seenAnAndDoc = query.seenAnOrDoc = true;

        ////////////////////////

        HashSet<String> result = new HashSet<>();
        Collections.addAll(result, "d2");
        Assert.assertEquals(result, query.buildResult());
    }

    @Test
    public void buildResultTest2(){

        query.andQueries.add("and1");       // d1,d2
        query.andQueries.add("and2");       // d1,d2

        query.excQueries.add("exc1");       // d1
        query.excQueries.add("exc2");       // d3

        query.seenAnAndDoc = true;

        HashSet<String> result = new HashSet<>();
        Collections.addAll(result, "d2");
        Assert.assertEquals(result, query.buildResult());
    }

    @Test
    public void buildResultTest3(){

        query.orQueries.add("or1");      // d1
        query.orQueries.add("or2");      // d2
        query.orQueries.add("or3");      // d3

        query.excQueries.add("exc1");       // d1

        query.seenAnOrDoc = true;

        HashSet<String> result = new HashSet<>();
        Collections.addAll(result, "d3", "d2");
        Assert.assertEquals(result, query.buildResult());
    }

    @Test
    public void buildResultTest4(){

        query.orQueries.add("or1");         // d1
        query.andQueries.add("and3");       // d4
        query.excQueries.add("exc2");       // d3

        query.seenAnOrDoc = query.seenAnAndDoc = true;

        HashSet<String> result = new HashSet<>();
        Assert.assertEquals(result, query.buildResult());
    }
}