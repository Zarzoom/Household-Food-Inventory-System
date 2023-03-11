const EditItem = () => {
    return (
        <div className="row">
            <div className="col-md-3 BlueBox">
                <p>
                    <label>Generic Name:</label><br/>
                    <input type="text" placeholder="beans"/><br/>
                    <label>Brand:</label><br/>
                    <input type="text" placeholder="World Famous Beans"/><br/>
                    <label>Size:</label><br/>
                    <input type="text" placeholder="3oz"/><br/>
                    <label>Price:</label><br/>
                    <input type="text" step="any" placeholder="0.00"/><br/>
                </p>
                <a className="btn btn-sm" href="#" role="button">Edit</a>
                <a className="btn btn-sm" href="#" role="button">Cancel</a>
            </div>
        </div>
    );
};
export default EditItem;