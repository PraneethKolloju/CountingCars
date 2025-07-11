'use client';
import React, { useEffect, useState } from 'react'
import AuctionCard from './AuctionCard';
import AppPagination from '@/components/AppPagination';
import { GetData } from '../actions/auctionActions';
import { Auction, PagedResult } from '@/types';
import Filters from './Filters';
import { useParamsStore } from '@/hooks/useParamsStore';
import { useShallow } from 'zustand/shallow';
import queryString from 'query-string';
import EmptyFilter from '@/components/EmptyFilter';

export default function Listings() {
    const [Data, setData] = useState<PagedResult<Auction>>();
    const Params = useParamsStore(useShallow(state => ({
        pageNumber: state.pageNumber,
        pageSize: state.pageSize,
        pageCount: state.pageCount,
        searchTerm: state.searchTerm,
        orderBy: state.orderBy,
        filterBy: state.filterBy
    })));

    const setParams = useParamsStore(state => state.setParams);
    const url = queryString.stringifyUrl({ url: '', query: Params }, { skipEmptyString: true })

    function SetPageNumber(pageNumber: number) {
        setParams({ pageNumber })
    }

    useEffect(() => {
        GetData(url).then(Data => {
            setData(Data)
        })
    }, [url]);

    return (
        <>
            <div>
                <Filters />
            </div>

            {Data?.pageCount == 0 ? (
                <EmptyFilter showReset={true} />
            ) : (
                <div>
                    <div className="grid grid-cols-4 gap-6">
                        {
                            Data && Data.results.map((auction) => (
                                <AuctionCard key={auction.id} auction={auction} />
                            ))
                        }
                    </div>
                    <div className='flex justify-center mb-5'>
                        <AppPagination onPageChange={SetPageNumber} currentPage={Params.pageNumber} pageCount={Params.pageCount} />
                    </div>
                </div>
            )}


        </>
    )
}
