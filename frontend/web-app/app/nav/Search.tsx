'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import { HtmlContext } from 'next/dist/server/route-modules/pages/vendored/contexts/entrypoints';
import React, { ChangeEvent, useState } from 'react'
import { FaSearch } from 'react-icons/fa';
export default function Search() {

    const setParams = useParamsStore(state => state.setParams);

    const [searchTerm, setSearchTerm] = useState('');


    function handleChange(e: ChangeEvent<HTMLInputElement>) {
        setSearchTerm(e.target.value);
    }

    function handleSearch() {
        setParams({ searchTerm: searchTerm })
    }


    return (
        <div className='flex w-[50%] items-center border-2 rounded-full py-2 shadow-sm'>
            <input
                onChange={handleChange}
                onKeyDown={(e: any) => {
                    if (e.key === 'Enter') handleSearch();
                }}
                type="text"
                value={searchTerm}
                placeholder='Search for cars by make, model or color'
                className='
                input-custom
                text-sm
                text-gray-600   
                '
            />
            <button>
                <FaSearch size={34}
                    onClick={handleSearch}
                    className='bg-red-400 text-white rounded-full p-2 cursor-pointer mx-2'
                />
            </button>
        </div>
    )
}
