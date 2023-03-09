const ItemDisplay = () => {
    return (
        <div className="row">
            <div className="col-md-3" style={{margin: "1em", backgroundColor: "rgba(143, 184,255, 0.5)"}}>
                <p>
                    Generic Name:<br>Brand Name:<br>Size:<br>Price:</br></br></br>
                </p>
                <a className="btn btn-sm" style={{backgroundColor: "rgba(255,214,143)"}} href="#" role="button">Edit</a>
                <a className="btn btn-sm" style={{backgroundColor: "rgba(255,214,143)"}} href="#"
                   role="button">Delete</a>
            </div>
        </div>
    );
};
export default ItemDisplay;