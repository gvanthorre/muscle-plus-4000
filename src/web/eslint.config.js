import js from "@eslint/js";
import globals from "globals";
import react from "eslint-plugin-react";
import reactHooks from "eslint-plugin-react-hooks";
import reactRefresh from "eslint-plugin-react-refresh";
import prettier from "eslint-plugin-prettier";
import tseslint from "typescript-eslint";

export default [
    {
        ignores: ["dist", "node_modules"],
    },

    js.configs.recommended,

    ...tseslint.configs.recommended,

    {
        files: ["**/*.{ts,tsx}"],
        languageOptions: {
            globals: globals.browser,
        },
        plugins: {
            react,
            "react-hooks": reactHooks,
            "react-refresh": reactRefresh,
            prettier,
        },
        rules: {
            ...react.configs.recommended.rules,
            ...reactHooks.configs.recommended.rules,

            "react/react-in-jsx-scope": "off",
            "react-refresh/only-export-components": [
                "warn",
                { allowConstantExport: true },
            ],
            "prettier/prettier": "warn",
        },
        settings: {
            react: {
                version: "detect",
            },
        },
    },
];
