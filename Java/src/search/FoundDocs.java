package search;

import java.util.HashSet;

public class FoundDocs {
    boolean isNull;
    HashSet<String> foundDocs;

    public boolean isNull() {
        return isNull;
    }

    public HashSet<String> getFoundDocs() {
        return foundDocs;
    }

    public FoundDocs(boolean isNull, HashSet<String> foundDocs) {
        this.isNull = isNull;
        this.foundDocs = foundDocs;
    }

}