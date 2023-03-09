const SearchItem = () => {
    return (
        <div className="row">
            <div className="col-md-3" style={{margin: "1em" ,backgroundColor: "rgba(143, 184,255, 0.5)"}}>
                <p>
                    <label>Search by Generic Name or Brand</label><br/>
                    <input type="text" placeholder="search"/><br/>
                </p>
                <a className="btn btn-sm" style={{backgroundColor: "rgba(255,214,143)"}} href="#" role="button">Search</a>
                <a className="btn btn-sm" style={{backgroundColor: "rgba(255,214,143)"}} href="#" role="button">Cancel</a>
            </div>
        </div>
    );
};
export default SearchItem;