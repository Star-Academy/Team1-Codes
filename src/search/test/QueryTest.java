package search.test;

import org.junit.*;

import static org.mockito.Mockito.*;

import search.*;

import java.util.*;

public class QueryTest {
    Query query;

    @Before
    public void setUp() {
        HashMap<String, HashSet<String>> allWords = new HashMap<>();

        InvertedIndex invertedIndex = mock(InvertedIndex.class);
        when(invertedIndex.getAllWords()).thenReturn(allWords);

        query = new Query(invertedIndex);

        HashSet<String> docs1 = new HashSet<>();
        docs1.add("d1");
        allWords.put("or1", docs1);

        HashSet<String> docs2 = new HashSet<>();
        docs2.add("d2");
        allWords.put("or2", docs2);

        HashSet<String> docs3 = new HashSet<>();
        docs3.add("d3");
        allWords.put("or3", docs3);

        HashSet<String> docs4 = new HashSet<>();
        docs4.add("d2");
        docs4.add("d1");
        allWords.put("and1", docs4);

        HashSet<String> docs5 = new HashSet<>();
        docs5.add("d1");
        docs5.add("d2");
        allWords.put("and2", docs5);

        HashSet<String> docs8 = new HashSet<>();
        docs8.add("d4");
        allWords.put("and3", docs8);

        HashSet<String> docs6 = new HashSet<>();
        docs6.add("d1");
        allWords.put("exc1", docs6);

        HashSet<String> docs7 = new HashSet<>();
        docs7.add("d3");
        allWords.put("exc2", docs7);
    }

    @Test
    public void analyzeQueryTest() {
        String[] input = {"+or1", "+or2", "and1", "+or3", "-exc1", "-exc2", "and2"};
        query.analyzeQuery(input);

        ArrayList<String> expectedOrQueries = new ArrayList<>(Arrays.asList("or1", "or2", "or3"));
        ArrayList<String> expectedAndQueries = new ArrayList<>(Arrays.asList("and1", "and2"));
        ArrayList<String> expectedExcQueries = new ArrayList<>(Arrays.asList("exc1", "exc2"));

        Assert.assertEquals(query.getOrQueries(), expectedOrQueries);
        Assert.assertEquals(query.getAndQueries(), expectedAndQueries);
        Assert.assertEquals(query.getExcQueries(), expectedExcQueries);
    }

    @Test
    public void buildResultTest1() {

        query.getOrQueries().addAll(Arrays.asList("or1", "or2", "or3"));    // d1, d2, d3
        query.getAndQueries().addAll(Arrays.asList("and1", "and2"));        // d1,d2 , d1,d2
        query.getExcQueries().addAll(Arrays.asList("exc1", "exc2"));        // d1, d3

        query.setSeenAnAndDoc(true);
        query.setSeenAnOrDoc(true);

        HashSet<String> result = new HashSet<>();
        Collections.addAll(result, "d2");
        Assert.assertEquals(result, query.buildResult());
    }

    @Test
    public void buildResultTest2() {

        query.getAndQueries().addAll(Arrays.asList("and1", "and2"));    // d1,d2 , d1,d2
        query.getExcQueries().addAll(Arrays.asList("exc1", "exc2"));    // d1, d3

        query.setSeenAnAndDoc(true);

        HashSet<String> result = new HashSet<>();
        Collections.addAll(result, "d2");
        Assert.assertEquals(result, query.buildResult());
    }

    @Test
    public void buildResultTest3() {

        query.getOrQueries().addAll(Arrays.asList("or1", "or2", "or3"));    // d1, d2, d3
        query.getExcQueries().add("exc1");  // d1

        query.setSeenAnOrDoc(true);

        HashSet<String> result = new HashSet<>();
        Collections.addAll(result, "d3", "d2");
        Assert.assertEquals(result, query.buildResult());
    }

    @Test
    public void buildResultTest4() {

        query.getOrQueries().add("or1");    // d1
        query.getAndQueries().add("and3");  // d4
        query.getExcQueries().add("exc2");  // d3

        query.setSeenAnOrDoc(true);
        query.setSeenAnAndDoc(true);

        HashSet<String> result = new HashSet<>();
        Assert.assertEquals(result, query.buildResult());
    }
}