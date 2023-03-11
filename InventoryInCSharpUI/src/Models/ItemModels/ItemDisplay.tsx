const ItemDisplay = () => {
    return (
        <div className="col-md-3">
            <div className="BlueBox">
                <p>
                    Generic Name:<br/>Brand Name:<br/>Size:<br/>Price:
                </p>
                <a className="btn btn-sm" href="#" role="button">Edit</a>
                <a className="btn btn-sm" href="#"
                   role="button">Delete</a>
            </div>
        </div>
    );
};
export default ItemDisplay;