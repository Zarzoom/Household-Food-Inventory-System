import "./../styles/main.less";
import path = require("node:path");

module.exports = {
    module: {
        rules: [
            {
                test: /\.less$/i,
                include: path.join('src/StyleSheet', 'src/components'),
                use: [
                    // compiles Less to CSS
                    "style-loader",
                    "css-loader",
                    "less-loader",
                    {
                        loader: 'typings-for-css-modules-loader',
                        options: {
                            modules: true,
                            namedExport: true
                        }
                    }
                ],
            },
        ],
    },
};