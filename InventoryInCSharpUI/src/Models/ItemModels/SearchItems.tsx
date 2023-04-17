const SearchItem = () => {
    return (
        <div className="row">
            <div className="col-md-3 BlueBox">
                <p>
                    <label>Search by Generic Name or Brand</label><br/>
                    <input type="text" placeholder="search"/><br/>
                </p>
                <a className="btn btn-sm" href="#" role="button">Search</a>
                <a className="btn btn-sm" href="#" role="button">Cancel</a>
            </div>
        </div>
    );
};
export default SearchItem;