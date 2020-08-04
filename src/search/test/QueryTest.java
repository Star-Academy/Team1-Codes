package search.test;

import org.junit.*;
import search.*;
import java.util.*;

public class QueryTest{

    @Test
    public void analyzeQueryTest(){
        Query query = new Query(new InvertedIndex());
        String[] input = {"+or1" , "+or2", "and1", "+or3", "-exc1", "-exc2", "and2"};
        query.analyzeQuery(input);
        ArrayList<String> expectedOrQueries = new ArrayList<>();
        expectedOrQueries.add("+or1");
        expectedOrQueries.add("+or2");
        expectedOrQueries.add("+or3");
        ArrayList<String> expectedAndQueries = new ArrayList<>();
        expectedOrQueries.add("and1");
        expectedOrQueries.add("and2");
        ArrayList<String> expectedExcQueries = new ArrayList<>();
        expectedOrQueries.add("-exc1");
        expectedOrQueries.add("-exc2");

        Assert.assertEquals(query.orQueries.size(), expectedOrQueries.size());
        Assert.assertEquals(query.andQueries.size(), expectedAndQueries.size());
        Assert.assertEquals(query.excQueries.size(), expectedExcQueries.size());

        for(int i=0; i<expectedOrQueries.size(); i++)
            Assert.assertEquals(query.orQueries.get(i), expectedOrQueries.get(i));

        for(int i=0; i<expectedAndQueries.size(); i++)
            Assert.assertEquals(query.andQueries.get(i), expectedAndQueries.get(i));

        for(int i=0; i<expectedExcQueries.size(); i++)
            Assert.assertEquals(query.excQueries.get(i), expectedExcQueries.get(i));

    }       
}